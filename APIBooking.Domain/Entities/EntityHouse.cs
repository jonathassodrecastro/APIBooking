﻿using System.Text.Json.Serialization;

namespace APIBooking.Domain.Entities
{
    public class EntityHouse
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("available")]
        public int Available { get; set; }

    }
}
