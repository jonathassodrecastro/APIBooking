﻿using System.Text.Json.Serialization;

namespace APIBooking.Domain.Entities
{
    public class EntityClient
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("LastName")]
        public string LastName { get; set; }
        [JsonPropertyName("Age")]
        public int Age{ get; set; }
        [JsonPropertyName("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
