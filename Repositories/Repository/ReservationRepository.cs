using APIBooking.Data.Context;
using APIBooking.Domain.Entities;
using Repositories.Base;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class ReservationRepository : BaseRepository<EntityReservation>, IReservationRepository
    {
        public ReservationRepository(DataContext context) : base(context)
        {
        }
    }
}
