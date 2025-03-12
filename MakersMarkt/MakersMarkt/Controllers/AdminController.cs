using Microsoft.AspNetCore.Mvc;
using MakersMarkt.Database;
using MakersMarkt.Database.Models.DTO;
using System.Linq;

namespace MakersMarkt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // PUT: api/<AdminController/VerifyAccount>
        [HttpPut("VerifyAccount")]
        public IActionResult VerifyAccount(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                user.IsVerified = true;
                db.SaveChanges();
                return Ok();
            }
        }

        // GET: api/<AdminController/AllReportedProducts>
        [HttpGet("AllReportedProducts")]
        public IActionResult AllReportedProducts()
        {
            using (var db = new AppDbContext())
            {
                var reportedProducts = db.Products
                    .Where(p => p.Reports > 0)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        ProductTypeId = p.ProductTypeId,
                        ProductTypeName = p.ProductType.Name,
                        UserId = p.UserId,
                        UserName = p.User.Username,
                        Price = p.Price,
                        IsFlagged = p.IsFlagged,
                        Reports = p.Reports
                    })
                    .ToList();

                return Ok(reportedProducts);
            }
        }

        //GET: api/<AdminController/GetAverageUserRating>
        [HttpGet("GetAverageUserRating")]
        public IActionResult GetAverageUserRating()
        {
            float rating = 0;
            using (var db = new AppDbContext())
            {
                var ratings = db.Users.Select(u => u.Rating).ToList();
                if (ratings.Any())
                {
                    rating = ratings.Average();
                }
            }
            return Ok(rating);
        }

        //GET: api/<AdminController/GetAllProductTypes>
        [HttpGet("GetAllProductTypes")]
        public IActionResult GetAllProductTypes()
        {
            using (var db = new AppDbContext())
            {
                var productTypes = db.ProductTypes.ToList();
                return Ok(productTypes);
            }
        }

        //GET: api/<<AdminController/GetPopularTypes>
        [HttpGet("GetPopularTypes")]
        public IActionResult GetPopularTypes()
        {
            using (var db = new AppDbContext())
            {
                var popularTypes = db.ProductTypes
                    .Select(pt => new
                    {
                        ProductType = pt,
                        ProductCount = db.Products.Count(p => p.ProductTypeId == pt.Id)
                    })
                    .OrderByDescending(pt => pt.ProductCount)
                    .Select(pt => new
                    {
                        pt.ProductType.Id,
                        pt.ProductType.Name,
                    })
                    .ToList();

                return Ok(popularTypes);
            }
        }

        //GET: api/<AdminController/GetPopularProducts>
        [HttpGet("GetPopularProducts")]
        public IActionResult GetPopularProducts()
        {
            using (var db = new AppDbContext())
            {
                var popularProducts = db.TradeProducts
                    .GroupBy(tp => tp.Product)
                    .Select(g => new
                    {
                        Product = g.Key,
                        TradeCount = g.Count()
                    })
                    .OrderByDescending(p => p.TradeCount)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Product.Id,
                        Name = p.Product.Name,
                        Description = p.Product.Description,
                        ProductTypeId = p.Product.ProductTypeId,
                        ProductTypeName = p.Product.ProductType.Name,
                        UserId = p.Product.UserId,
                        UserName = p.Product.User.Username,
                        Price = p.Product.Price,
                        IsFlagged = p.Product.IsFlagged,
                        Reports = p.Product.Reports
                    })
                    .ToList();

                return Ok(popularProducts);
            }
        }


    }
}
