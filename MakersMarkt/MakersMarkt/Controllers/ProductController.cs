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
                var result = await db.Products // Get all the products from the database.
                            .Include(product => product.User)
                                .ThenInclude(user => user.Role)
                            .Include(product => product.ProductType)
                            .ToListAsync();

                return Ok(result); // Return the list of products.
            }
        }

        // Get a specific product by its id.
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Product? result = await db.Products // Get the product with the specified id.
                            .Where(product => product.Id == id)
                            .Include(product => product.User)
                                .ThenInclude(user => user.Role)
                            .Include(product => product.ProductType)
                            .FirstOrDefaultAsync();

                if (result == null) // If the product that is being searched for, does not exist, return a 404.
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
            if (name == null || description == null) // If the name or description is null, return a 400.
            {
                return BadRequest("Invalid Input");
            }

            using (AppDbContext db = new AppDbContext())
            {
                User? user = await db.Users.FindAsync(userId); // Find the user with the specified id.
                if (user == null) // If the user does not exist, return a 400.
                {
                    return BadRequest("User does not exist");
                }

                ProductType? productType = await db.ProductTypes.FindAsync(productTypeId); // Find the product type with the specified id.
                if (productType == null) // If the product type does not exist, return a 400.
                {
                    return BadRequest("Product Type does not exist");
                }


                Product product = new Product // Create a new product with the specified parameters.
                {
                    Name = name,
                    Description = description,
                    ProductTypeId = productTypeId,
                    UserId = userId,
                    Price = price,
                    IsFlagged = isFlagged,
                    Reports = reports
                };
                db.Products.Add(product); // Add the product to the database.
                await db.SaveChangesAsync(); // Save the changes to the database.

                return Ok(product); // Return the product that was created.
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> EditProduct(int id, string name, string description, int productTypeId, int userId, decimal price, bool isFlagged, int reports)
        {
            if (name == null || description == null) // If the name or description is null, return a 400.
            {
                return BadRequest("Invalid Input");
            }

            Product? product;
            using (AppDbContext db = new AppDbContext()) // Find the product with the specified id.
            {
                product = db.Products.Find(id);
                if (product == null) // If the product does not exist, return a 400.
                {
                    return BadRequest("Product does not exist");
                }

                User? user = await db.Users.FindAsync(userId); // Find the user with the specified id.
                if (user == null) // If the user does not exist, return a 400.
                {
                    return BadRequest("User does not exist");
                }

                ProductType? productType = await db.ProductTypes.FindAsync(productTypeId); // Find the product type with the specified id.
                if (productType == null) // If the product type does not exist, return a 400.
                {
                    return BadRequest("Product Type does not exist");
                }
                // Update the product with the new parameters.
                product.Name = name;
                product.Description = description;
                product.ProductTypeId = productType.Id;
                product.UserId = userId;
                product.IsFlagged = isFlagged;
                product.Reports = reports;
                db.UpdateRange(product); // Update the product in the database.
                await db.SaveChangesAsync(); // Save the changes to the database.

                return Ok(product); // Return the updated product.

            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            using (AppDbContext db = new AppDbContext()) 
            {
                // Find the product with the specified id.
                Product? product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return BadRequest("Product not found");
                }
                db.RemoveRange(product); // Remove the product from the database.
                await db.SaveChangesAsync();
                return Ok(); // Return a 200.
            }
        }

        [HttpPut("{id}/report")]
        public async Task<ActionResult<Product>> Report(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                // Find the product with the specified id.
                Product? product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return BadRequest("Product not found");
                }
                // Increment the reports of the product.
                product.Reports++;
                db.UpdateRange(product);
                // Save the changes to the database and return a 200.
                await db.SaveChangesAsync();
                return Ok(product);
            }
        }
    }
}
