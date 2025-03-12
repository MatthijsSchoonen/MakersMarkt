using MakersMarkt.Database;
using MakersMarkt.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MakersMarkt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        // Returns all the products currently in the database with the user.
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var result = await db.Products
                            .Include(product => product.User)
                                .ThenInclude(user => user.Role)
                            .Include(product => product.ProductType)
                            .ToListAsync();

                return Ok(result);
            }
        }

        // Get a specific product by its id.
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Product? result = await db.Products
                            .Where(product => product.Id == id)
                            .Include(product => product.User)
                                .ThenInclude(user => user.Role)
                            .Include(product => product.ProductType)
                            .FirstOrDefaultAsync();

                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
        }

        // Create a new Product
        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(string name, string description, int productTypeId, int userId, decimal price, bool isFlagged, int reports)
        {
            if (name == null || description == null)
            {
                return BadRequest("Invalid Input");
            }

            using (AppDbContext db = new AppDbContext())
            {
                User? user = await db.Users.FindAsync(userId);
                if (user == null)
                {
                    return BadRequest("User does not exist");
                }

                ProductType? productType = await db.ProductTypes.FindAsync(productTypeId);
                if (productType == null)
                {
                    return BadRequest("Product Type does not exist");
                }


                Product product = new Product
                {
                    Name = name,
                    Description = description,
                    ProductTypeId = productTypeId,
                    UserId = userId,
                    Price = price,
                    IsFlagged = isFlagged,
                    Reports = reports
                };
                db.Products.Add(product);
                await db.SaveChangesAsync();

                return Ok(product);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> EditProduct(int id, string name, string description, int productTypeId, int userId, decimal price, bool isFlagged, int reports)
        {
            if (name == null || description == null)
            {
                return BadRequest("Invalid Input");
            }

            Product? product;
            using (AppDbContext db = new AppDbContext())
            {
                product = db.Products.Find(id);
                if (product == null)
                {
                    return BadRequest("Product does not exist");
                }

                User? user = await db.Users.FindAsync(userId);
                if (user == null)
                {
                    return BadRequest("User does not exist");
                }

                ProductType? productType = await db.ProductTypes.FindAsync(productTypeId);
                if (productType == null)
                {
                    return BadRequest("Product Type does not exist");
                }

                product.Name = name;
                product.Description = description;
                product.ProductTypeId = productType.Id;
                product.UserId = userId;
                product.IsFlagged = isFlagged;
                product.Reports = reports;
                db.UpdateRange(product);
                await db.SaveChangesAsync();

                return Ok(product);

            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Product? product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return BadRequest("Product not found");
                }
                db.RemoveRange(product);
                await db.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPut("{id}/report")]
        public async Task<ActionResult<Product>> Report(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Product? product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return BadRequest("Product not found");
                }
                product.Reports++;
                db.UpdateRange(product);
                await db.SaveChangesAsync();
                return Ok(product);
            }
        }
    }
}
