namespace MakersMarkt.Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required decimal Balance { get; set; }
        public required bool AllowEmails { get; set; }
        public required bool IsVerified { get; set; }
        public required int LoginAttempts { get; set; }
        public DateTime? LoginBlockedAt { get; set; }
        public required float Rating { get; set; }

        public required int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
