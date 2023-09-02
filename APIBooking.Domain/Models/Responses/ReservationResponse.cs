using System.Text.Json.Serialization;

namespace APIBooking.Domain.Models.Responses
{
    public class ReservationResponse
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("clientId")]
        public int ClientId { get; set; }

        [JsonPropertyName("clientName")]
        public string ClientName { get; set; }

        [JsonPropertyName("clientLastname")]
        public string ClientLastName { get; set; }

        [JsonPropertyName("clientFullName")]
        public string ClientFullName { get; set; }

        [JsonPropertyName("clientAge")]
        public int ClientAge { get; set; }

        [JsonPropertyName("clientPhoneNumber")]
        public string ClientPhoneNumber { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("houseId")]
        public int HouseId { get; set; }

        [JsonPropertyName("discountCode")]
        public string DiscountCode { get; set; }
    }
}
