namespace MakersMarkt.Database.Models
{
    public class UserNotification
    {
        public int Id { get; set; }

        public required int UserId { get; set; }
        public User User { get; set; }

        public required string Title { get; set; }
        public required string Text { get; set; }
    }
}
