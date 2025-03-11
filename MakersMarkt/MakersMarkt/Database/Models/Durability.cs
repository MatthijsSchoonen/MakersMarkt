namespace MakersMarkt.Database.Models
{
    public class Durability
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int LifeTime { get; set; }
        public required string Description { get; set; }
    }
}
