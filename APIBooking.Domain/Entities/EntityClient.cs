using System.Text.Json.Serialization;

namespace APIBooking.Domain.Entities
{
    public class EntityClient
    {
        [JsonPropertyName("client_id")]
        public int id { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("lastname")]
        public string lastname { get; set; }
        [JsonPropertyName("age")]
        public int age{ get; set; }
        [JsonPropertyName("phoneNumber")]
        public string phoneNumber { get; set; }
    }
}
