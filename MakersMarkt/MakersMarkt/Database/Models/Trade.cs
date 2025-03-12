using System.Text.Json.Serialization;

namespace MakersMarkt.Database.Models
{
    public class Trade
    {
        public int Id { get; set; }

        public required int SenderId { get; set; }
        public User Sender { get; set; }

        public required int RecipientId { get; set; }
        public User Recipient { get; set; }

        public int StatusId { get; set; }
        [JsonIgnore]
        public ICollection<TradeProduct> TradeProducts { get; set; }
    }
}
