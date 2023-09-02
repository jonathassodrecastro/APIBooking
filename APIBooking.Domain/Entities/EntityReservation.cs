using APIBooking.Domain.Models.Requests;
using System.Text.Json.Serialization;

namespace APIBooking.Domain.Entities
{
    public class EntityReservation
    {
        [JsonPropertyName("Reservation_id")]
        public int Id { get; set; }
        [JsonPropertyName("ClientId")]
        public int ClientId { get; set; }
        [JsonPropertyName("ClientName")]
        public string ClientName { get; set; }
        [JsonPropertyName("ClientLastname")]
        public string ClientLastName { get; set; }
        [JsonPropertyName("ClientAge")]
        public int ClientAge { get; set; }
        [JsonPropertyName("ClientPhoneNumber")]
        public string ClientPhoneNumber { get; set; }
        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("HouseId")]
        public int HouseId { get; set; }
        [JsonPropertyName("DiscountCode")]
        public string DiscountCode { get; set; }

        public void Update(UpdateReservationRequest request)
        {
            ClientId = request.ClientId;
            ClientName = request.ClientName;
            ClientLastName = request.ClientLastName;
            ClientAge = request.ClientAge;
            ClientPhoneNumber = request.ClientPhoneNumber;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
            HouseId = request.HouseId;
            DiscountCode = request.DiscountCode;
        }

    }
}
