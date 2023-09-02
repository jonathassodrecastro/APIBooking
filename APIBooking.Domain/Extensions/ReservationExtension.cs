using APIBooking.Domain.Entities;
using APIBooking.Domain.Models.Responses;

namespace APIBooking.Domain.Extensions
{
    public static class ReservationExtension
    {
        public static ReservationResponse ToResponse(this EntityReservation entity) 
        {
            return new ReservationResponse
            {
                Id = entity.id,
                ClientId = entity.clientId,
                ClientName = entity.clientName,
                ClientLastName = entity.clientLastname,
                ClientFullName = $"{entity.clientName} {entity.clientLastname}",
                ClientAge = entity.clientAge,
                ClientPhoneNumber = entity.clientPhoneNumber,
                StartDate = entity.startDate,
                EndDate = entity.endDate,
                HouseId = entity.houseId,
                DiscountCode = entity.discountCode
            };
        }
    }
}
