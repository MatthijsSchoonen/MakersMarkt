namespace MakersMarkt.Database.Models
{
    public class PasswordReset
    {
        public int Id { get; set; }

        public required int UserId { get; set; }
        public User User { get; set; }

        public required string Code { get; set; }

        public required DateTime CreatedAt { get; set; }

        public PasswordReset()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
