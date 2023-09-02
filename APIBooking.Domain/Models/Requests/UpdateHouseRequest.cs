using System.Text.Json.Serialization;

namespace APIBooking.Domain.Models.Requests
{
    public class UpdateHouseRequest
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("available")]
        public int Available { get; set; }
    }
}
