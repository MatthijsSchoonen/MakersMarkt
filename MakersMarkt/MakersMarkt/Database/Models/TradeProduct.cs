namespace MakersMarkt.Database.Models
{
    public class TradeProduct
    {
        public int Id { get; set; }
        
        public required int ProductId { get; set; }
        public Product Product { get; set; }

        public required int TradeId { get; set; }
        public Trade Trade { get; set; }

    }
}
