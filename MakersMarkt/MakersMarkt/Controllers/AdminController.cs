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
                return NotFound(404);
            }
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(id);
                if (user == null)
                {
                    return NotFound(404);
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
    }
}
