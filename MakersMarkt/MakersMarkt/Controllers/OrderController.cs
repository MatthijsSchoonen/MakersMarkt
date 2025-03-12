using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakersMarkt.Database;
using MakersMarkt.Database.Models;

namespace MakersMarkt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // GET: api/Order/GetAllOrder
        [HttpGet("GetAllOrder")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrder()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return await db.Orders
                    .Include(order => order.Product)
                        .ThenInclude(product => product.ProductType)
                    .Include(order => order.Product)
                        .ThenInclude(product => product.User)
                    .Include(order => order.Status)
                    .Include(order => order.Buyer)
                        .ThenInclude(buyer => buyer.Role)
                    .Include(order => order.Seller)
                        .ThenInclude(seller => seller.Role)
                    .ToListAsync();
            }
        }

        // GET: api/Order/GetOrderById
        [HttpGet("GetOrderById")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var order = await db.Orders
                    .Include(o => o.Product)
                        .ThenInclude(p => p.ProductType)
                    .Include(o => o.Product)
                        .ThenInclude(p => p.User)
                    .Include(o => o.Status)
                    .Include(o => o.Buyer)
                        .ThenInclude(b => b.Role)
                    .Include(o => o.Seller)
                        .ThenInclude(s => s.Role)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                {
                    return NotFound();
                }

                return order;
            }
        }

        // PUT: api/Order/EditOrder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("EditOrder")]
        public async Task<IActionResult> EditOrder(int id, Order order)
        {
            using (AppDbContext db = new AppDbContext())
            {
                if (id != order.Id)
                {
                    return BadRequest();
                }

                db.Entry(order).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
        }

        // POST: api/Order/CreateOrder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();

                return CreatedAtAction("GetOrder", new { id = order.Id }, order);
            }
        }

        // DELETE: api/Order/DeleteOrder
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var order = await db.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                db.Orders.Remove(order);
                await db.SaveChangesAsync();

                return NoContent();
            }
        }

        private bool OrderExists(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.Orders.Any(e => e.Id == id);
            }
        }
    }
}
