using APIBooking.Data.DataConfig;
using APIBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIBooking.Data.Context
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EntityClient>(new ClientConfiguration().Configure);
            base.OnModelCreating(builder);

            builder.Entity<EntityHouse>(new HouseConfiguration().Configure);
            base.OnModelCreating(builder);

            builder.Entity<EntityReservation>(new ReservationConfiguration().Configure);
            base.OnModelCreating(builder);
        }
    }
}
