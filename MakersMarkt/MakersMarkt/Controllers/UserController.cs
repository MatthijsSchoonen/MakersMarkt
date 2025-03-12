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
        private static int _maxLoginAttempts = 3;

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            using (var db = new AppDbContext())
            {
                // Get all users
                var users = db.Users
                    .Include(u => u.Role)
                    .ToList();
                return Ok(users);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int userId)
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            using (var db = new AppDbContext())
            {
                // Get user by email
                var user = db.Users
                    .Where(u => u.Email == email)
                    .FirstOrDefault();

                // Check if user exists
                if (user == null)
                    return NotFound(new { message = "User not found." });

                // Check if user is blocked
                if (user.LoginBlockedAt != null && DateTime.Now <= user.LoginBlockedAt.Value.AddHours(1))
                    return BadRequest(new { message = "User is blocked from logging in." });

                // Check if password is correct
                if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password))
                {
                    // Increment login attempts
                    user.LoginAttempts++;

                    // Check if user should be blocked
                    if (user.LoginAttempts >= _maxLoginAttempts)
                    {
                        user.LoginBlockedAt = DateTime.Now;
                        user.LoginAttempts = 0;
                    }

                    // Update user
                    db.Users.Update(user);
                    await db.SaveChangesAsync();

                    // Check if user has been blocked
                    if (user.LoginAttempts >= _maxLoginAttempts)
                    {
                        // Too many failed login attempts
                        return BadRequest(new { message = "Too many failed login attempts, user has been blocked." });
                    }

                    // Incorrect password
                    return BadRequest(new { message = "Incorrect password" });
                }

                // Reset login attempts
                user.LoginAttempts = 0;

                // Update user
                db.Users.Update(user);
                await db.SaveChangesAsync();

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
                    LoginAttempts = 0,
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
