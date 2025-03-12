using MakersMarkt.Database;
using MakersMarkt.Database.Models;
using MakersMarkt.Database.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MakersMarkt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            using (var db = new AppDbContext())
            {
                // Find user by id
                var user = db.Users
                    .Where(u => u.Id == userId)
                    .Include(u => u.Role)
                    .FirstOrDefault();

                // Check if user exists
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            using (var db = new AppDbContext())
            {
                // Validate if username is unique
                if (db.Users.Any(u => u.Username == user.Username))
                    return BadRequest("Username already exists.");

                // Validate if email is unique
                if (db.Users.Any(u => u.Email == user.Email))
                    return BadRequest("Email already exists.");

                // Add user to database
                db.Users.Add(new()
                {
                    Username = user.Username,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password),
                    Email = user.Email,
                    Balance = user.Balance,
                    RoleId = user.RoleId,
                    AllowEmails = user.AllowEmails,
                    LoginBlockedAt = null,
                    IsVerified = false,
                    Rating = 0
                });
                await db.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            using (var db = new AppDbContext())
            {
                // Find user by id
                var existingUser = db.Users
                    .Where(u => u.Id == user.Id)
                    .Include(u => u.Role)
                    .FirstOrDefault();

                // Check if user exists
                if (existingUser == null)
                    return NotFound();

                // Validate if username is unique
                if (db.Users.Any(u => u.Username == user.Username && u.Id != user.Id))
                    return BadRequest("Username already exists.");

                // Validate if email is unique
                if (db.Users.Any(u => u.Email == user.Email && u.Id != user.Id))
                    return BadRequest("Email already exists.");

                // Update user properties
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;
                existingUser.Balance = user.Balance;
                existingUser.RoleId = user.RoleId;
                existingUser.AllowEmails = user.AllowEmails;
                existingUser.IsVerified = user.IsVerified;
                existingUser.Rating = user.Rating;

                // Save changes to database
                db.Users.Update(existingUser);
                await db.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            using (var db = new AppDbContext())
            {
                // Find user by id
                var user = db.Users.Find(userId);

                // Check if user exists
                if (user == null)
                    return NotFound();

                // Remove user from database
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
