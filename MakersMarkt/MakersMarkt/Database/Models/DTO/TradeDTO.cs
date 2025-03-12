namespace MakersMarkt.Database.Models.DTO
{
    public class TradeDTO
    {
        public int Id { get; set; }
        public UserDTO Sender { get; set; }
        public UserDTO Recipient { get; set; }
        public int StatusId { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
