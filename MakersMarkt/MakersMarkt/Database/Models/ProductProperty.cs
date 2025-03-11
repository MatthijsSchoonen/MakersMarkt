namespace MakersMarkt.Database.Models
{
    public class ProductProperty
    {
        public int Id { get; set; }

        public required int ProductId { get; set; }
        public Product Product { get; set; }

        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}
