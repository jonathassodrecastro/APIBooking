using APIBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Reservation
{
    public interface IReservationServices
    {
        EntityReservation GetEntityReservation(int id);

        
    }
}
