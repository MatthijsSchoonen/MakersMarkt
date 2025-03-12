    namespace MakersMarkt.Database.Models.DTO
    {
        public class UserDTO
        {
            public int Id { get; set; }
            public required string Username { get; set; }
            public required string Password { get; set; }
            public required string Email { get; set; }
            public required decimal Balance { get; set; }
            public required bool AllowEmails { get; set; }
            public required bool IsVerified { get; set; }
            public required float Rating { get; set; }

            public required int RoleId { get; set; }
        }
    }
