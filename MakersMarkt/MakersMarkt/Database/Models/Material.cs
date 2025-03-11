namespace MakersMarkt.Database.Models
{
    public class Material
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Amount { get; set; }
    }
}
