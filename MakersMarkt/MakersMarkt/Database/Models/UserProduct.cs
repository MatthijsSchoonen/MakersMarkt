namespace MakersMarkt.Database.Models
{
    public class UserProduct
    {
        public int Id { get; set; }
        
        public required int UserId { get; set; }
        public User User { get; set; }

        public required int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
