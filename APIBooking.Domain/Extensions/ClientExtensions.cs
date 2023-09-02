using APIBooking.Domain.Entities;
using APIBooking.Domain.Models.Responses;

namespace APIBooking.Domain.Extensions
{
    public static class ClientExtensions
    {
        public static ClientResponse ToResponse(this EntityClient entity) 
        {
            return new ClientResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                Age = entity.Age,
                PhoneNumber = entity.PhoneNumber
            };
        }
    }
}
