using APIBooking.Domain.Entities;
using APIBooking.Domain.Models.Responses;

namespace APIBooking.Domain.Extensions
{
    public static class HouseExtensions
    {
        public static HouseResponse ToResponse(this EntityHouse entity) 
        {
            return new HouseResponse
            {
                Id = entity.Id,
                Available = entity.Available
            };
        }
    }
}
