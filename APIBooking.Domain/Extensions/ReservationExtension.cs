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
                Id = entity.Id,
                ClientId = entity.ClientId,
                ClientName = entity.ClientName,
                ClientLastName = entity.ClientLastName,
                ClientFullName = $"{entity.ClientName} {entity.ClientLastName}",
                ClientAge = entity.ClientAge,
                ClientPhoneNumber = entity.ClientPhoneNumber,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                HouseId = entity.HouseId,
                DiscountCode = entity.DiscountCode
            };
        }
    }
}
