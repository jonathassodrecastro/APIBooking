using APIBooking.Data.Context;
using APIBooking.Domain.Entities;
using Repositories.Base;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class ClientRepository : BaseRepository<EntityClient>, IClientRepository
    {
        public ClientRepository(DataContext context) : base(context)
        {
        }
    }
}
