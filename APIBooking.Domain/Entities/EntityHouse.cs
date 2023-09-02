using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIBooking.Domain.Entities
{
    public class EntityHouse
    {
        [JsonPropertyName("house_id")]
        public int id { get; set; }
        [JsonPropertyName("available")]
        public int available { get; set; }

    }
}
