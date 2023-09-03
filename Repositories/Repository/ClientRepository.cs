using APIBooking.Data.Context;
using APIBooking.Domain.Entities;
using Repositories.Base;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class ClientRepository : BaseRepository<EntityClient>, IClientRepository
    {
        public ClientRepository(DataContext context) : base(context)
        {
        }
    }
}
