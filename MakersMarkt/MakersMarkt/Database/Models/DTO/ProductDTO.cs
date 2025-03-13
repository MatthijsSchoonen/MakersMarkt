namespace MakersMarkt.Database.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public required int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }

        public required int UserId { get; set; }
        public string UserName { get; set; }

        public required decimal Price { get; set; }
        public required bool IsFlagged { get; set; }

        public int Reports { get; set; }
    }
}
