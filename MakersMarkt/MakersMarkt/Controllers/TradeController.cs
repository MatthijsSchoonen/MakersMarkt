using MakersMarkt.Database;
using MakersMarkt.Database.Models;
using MakersMarkt.Database.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MakersMarkt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        // GET: api/<TradeController>
        [HttpGet]
        public IActionResult Get()
        {
            using (var db = new AppDbContext())
            {
                var trades = db.Trades
                    .Include(t => t.Sender)
                    .Include(t => t.Recipient)
                    .Include(t => t.TradeProducts)
                        .ThenInclude(tp => tp.Product)
                            .ThenInclude(p => p.ProductType) // Include ProductType
                    .ToList();

                // Mapping Trade to TradeDTO
                var tradeDTOs = trades.Select(t => new TradeDTO
                {
                    Id = t.Id,
                    Sender = new UserDTO
                    {
                        Id = t.Sender.Id,
                        Username = t.Sender.Username,
                        Password = "", // You can leave these fields empty or modify based on your requirements
                        Email = "",
                        Balance = 0,
                        AllowEmails = false,
                        IsVerified = false,
                        Rating = 0,
                        RoleId = t.Sender.RoleId
                    },
                    Recipient = new UserDTO
                    {
                        Id = t.Recipient.Id,
                        Username = t.Recipient.Username,
                        Password = "", // Same for the recipient
                        Email = "",
                        Balance = 0,
                        AllowEmails = false,
                        IsVerified = false,
                        Rating = 0,
                        RoleId = t.Recipient.RoleId
                    },
                    StatusId = t.StatusId,
                    Products = t.TradeProducts.Select(tp => new ProductDTO
                    {
                        Id = tp.Product.Id,
                        Name = tp.Product.Name,
                        Description = tp.Product.Description,
                        Price = tp.Product.Price,
                        IsFlagged = tp.Product.IsFlagged,
                        Reports = tp.Product.Reports,
                        ProductTypeName = tp.Product.ProductType.Name, // Include Product Type Name
                        UserName = tp.Product.User.Username // Include Product's Seller Username
                    }).ToList()
                }).ToList();

                return Ok(tradeDTOs);
            }
        }

        // GET api/<TradeController>/5
        [HttpGet("GetByID/{id}")]
        public IActionResult Get(int id)
        {
            using (AppDbContext db = new())
            {
                var trade = db.Trades
                    .Include(t => t.Sender)
                    .Include(t => t.Recipient)
                    .Include(t => t.TradeProducts)
                        .ThenInclude(tp => tp.Product)
                            .ThenInclude(p => p.ProductType) // Include ProductType
                    .FirstOrDefault(t => t.Id == id);

                if (trade == null)
                {
                    return NotFound();
                }

                // Mapping Trade to TradeDTO
                var tradeDTO = new TradeDTO
                {
                    Id = trade.Id,
                    Sender = new UserDTO
                    {
                        Id = trade.Sender.Id,
                        Username = trade.Sender.Username,
                        Password = "", // Same for sender's password
                        Email = "",
                        Balance = 0,
                        AllowEmails = false,
                        IsVerified = false,
                        Rating = 0,
                        RoleId = trade.Sender.RoleId
                    },
                    Recipient = new UserDTO
                    {
                        Id = trade.Recipient.Id,
                        Username = trade.Recipient.Username,
                        Password = "", // Same for recipient's password
                        Email = "",
                        Balance = 0,
                        AllowEmails = false,
                        IsVerified = false,
                        Rating = 0,
                        RoleId = trade.Recipient.RoleId
                    },
                    StatusId = trade.StatusId,
                    Products = trade.TradeProducts.Select(tp => new ProductDTO
                    {
                        Id = tp.Product.Id,
                        Name = tp.Product.Name,
                        Description = tp.Product.Description,
                        Price = tp.Product.Price,
                        IsFlagged = tp.Product.IsFlagged,
                        Reports = tp.Product.Reports,
                        ProductTypeName = tp.Product.ProductType.Name, // Include Product Type Name
                        UserName = tp.Product.User.Username // Include Product's Seller Username
                    }).ToList()
                };

                return Ok(tradeDTO);
            }
        }

        // POST api/<TradeController>
        [HttpPost]
        public IActionResult Post([FromBody] Trade trade)
        {
            if (trade == null)
            {
                return BadRequest(new { Message = "Invalid trade data." });
            }

            using (AppDbContext db = new())
            {
                db.Trades.Add(trade);
                db.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = trade.Id }, trade);
            }
        }

        // PUT api/<TradeController>/Edit
        [HttpPut("Edit/{id}")]
        public IActionResult Put(int id, [FromBody] int statusId)
        {
            using (AppDbContext db = new())
            {
                var trade = db.Trades.Find(id);
                if (trade == null)
                {
                    return NotFound();
                }
                trade.StatusId = statusId;
                db.SaveChanges();
                return Ok(trade);
            }
        }

        // DELETE api/<TradeController>/5
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            using (AppDbContext db = new())
            {
                var trade = db.Trades.Find(id);
                if (trade == null)
                {
                    return NotFound();
                }
                db.Trades.Remove(trade);
                db.SaveChanges();
                return NoContent();
            }
        }
    }
}
