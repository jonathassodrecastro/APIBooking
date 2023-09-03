using APIBooking.Data.Context;
using APIBooking.Domain.Entities;
using Repositories.Base;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class HouseRepository : BaseRepository<EntityHouse>, IHouseRepository
    {
        public HouseRepository(DataContext context) : base(context)
        {
        }
    }
}
