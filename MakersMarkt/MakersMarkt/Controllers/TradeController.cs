using Microsoft.AspNetCore.Mvc;
using MakersMarkt.Database;
using MakersMarkt.Database.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MakersMarkt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        // GET: api/<TradeController>
        [HttpGet]
        public IEnumerable<Trade> Get()
        {
            using (AppDbContext db = new())
            {
                var t = db.Trades
                    .Include(t => t.TradeProducts)
                    .ThenInclude(tp => tp.Product);
                return db.Trades.ToList();  
            }
        }

        // GET api/<TradeController>/5
        [HttpGet("GetByID")]
        public IActionResult Get(int id)
        {
            using (AppDbContext db = new())
            {
                var trade = db.Trades
                    .Include(t => t.TradeProducts)        
                    .ThenInclude(tp => tp.Product)      
                    .FirstOrDefault(t => t.Id == id);

                if (trade == null)
                {
                    return NotFound();
                }

                return Ok(trade);
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody] Trade trade)
        {
            if (trade == null)
            {
                return BadRequest(new { Message = "Invalid trade data." });
            }


            using (AppDbContext db = new())
            {
                var t = db.Trades
                    .Include(t => t.TradeProducts)
                    .ThenInclude(tp => tp.Product);

                db.Trades.Add(trade);
                db.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = trade.Id }, trade); 
            }
        }


        [HttpPut("Edit")]
        public IActionResult Put(int id, [FromBody] int statusId)
        {
            using (AppDbContext db = new())
            {
                var trade = db.Trades.Find(id);
                if (trade == null)
                {
                    return NotFound(); 
                }
                var t = db.Trades
                   .Include(t => t.TradeProducts)
                   .ThenInclude(tp => tp.Product)
                   .FirstOrDefault(t => t.Id == id);
                trade.StatusId = statusId;
                db.SaveChanges();
                return Ok(trade);
            }
        }


        // DELETE api/<TradeController>/5
        [HttpDelete("Delete")]
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
