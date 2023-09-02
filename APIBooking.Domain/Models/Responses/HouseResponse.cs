using System.Text.Json.Serialization;

namespace APIBooking.Domain.Models.Responses
{
    public class HouseResponse
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("available")]
        public int Available { get; set; }
    }
}
