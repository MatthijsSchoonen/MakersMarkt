namespace MakersMarkt.Database.Models
{
    public class Order
    {
        public int Id { get; set; }

        public required string Description { get; set; }
        public required int ProductId { get; set; }
        public Product Product { get; set; }   
        public required int StatusId { get; set; }
        public Status Status { get; set; }

        public required int BuyerId { get; set; }
        public User Buyer { get; set; }

        public required int SellerId { get; set; }
        public User Seller { get; set; }
    }
}
