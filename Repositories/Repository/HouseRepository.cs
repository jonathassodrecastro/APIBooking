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
    public class HouseRepository : BaseRepository<EntityHouse>, IHouseRepository
    {
        public HouseRepository(DataContext context) : base(context)
        {
        }
    }
}
