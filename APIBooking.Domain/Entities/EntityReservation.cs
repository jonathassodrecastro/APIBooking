using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBooking.Domain.Entities
{
    public class EntityReservation
    {
        public int id { get; set; }
        public int clientId { get; set; }
        public string clientName { get; set; }
        public string clientLastname { get; set; }
        public int clientAge { get; set; }
        public string clientPhoneNumber { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int houseId { get; set; }
        public string discountCode { get; set; }
    }
}
