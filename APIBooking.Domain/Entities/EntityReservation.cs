using APIBooking.Domain.Models.Request.Reservations;
using System.Text.Json.Serialization;

namespace APIBooking.Domain.Entities
{
    public class EntityReservation
    {
        [JsonPropertyName("reservation_id")]
        public int id { get; set; }
        [JsonPropertyName("clientId")]
        public int clientId { get; set; }
        [JsonPropertyName("clientName")]
        public string clientName { get; set; }
        [JsonPropertyName("clientLastname")]
        public string clientLastname { get; set; }
        [JsonPropertyName("clientAge")]
        public int clientAge { get; set; }
        [JsonPropertyName("clientPhoneNumber")]
        public string clientPhoneNumber { get; set; }
        [JsonPropertyName("startDate")]
        public DateTime startDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime endDate { get; set; }
        [JsonPropertyName("houseId")]
        public int houseId { get; set; }
        [JsonPropertyName("discountCode")]
        public string discountCode { get; set; }

        public void Update(UpdateReservationRequest request)
        {
            clientId = request.ClientId;
            clientName = request.ClientName;
            clientLastname = request.ClientLastName;
            clientAge = request.ClientAge;
            clientPhoneNumber = request.ClientPhoneNumber;
            startDate = request.StartDate;
            endDate = request.EndDate;
            houseId = request.HouseId;
            discountCode = request.DiscountCode;
        }
    }
}
