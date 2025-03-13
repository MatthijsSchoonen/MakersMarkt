namespace MakersMarkt.Database.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        
        public required int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public required int UserId { get; set; }
        public User User { get; set; }

        public required decimal Price { get; set; }
        public required bool IsFlagged { get; set; }

        public int Reports { get; set; }
        public string? Image { get; set; }
    }
}
