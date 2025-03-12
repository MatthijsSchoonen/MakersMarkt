namespace MakersMarkt.Database.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public   decimal Price { get; set; }
        public bool IsFlagged { get; set; }

        public int Reports { get; set; }
    }
}
