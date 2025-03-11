namespace MakersMarkt.Database.Models
{
    public class ProductSpecs
    {
        public int Id { get; set; }
        
        public required int ProductId { get; set; }
        public Product Product { get; set; }

        public required int MaterialId { get; set; }
        public Material Material { get; set; }

        public required int ProductionTime { get; set; }

        public required int ComplexityId { get; set; }
        public Complexity Complexity { get; set; }

        public required int DurabilityId { get; set; }
        public Durability Durability { get; set; }
    }
}
