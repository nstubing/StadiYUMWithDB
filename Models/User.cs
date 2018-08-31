using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentDBTodo.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]

        public string Id { get; set; }
        [JsonProperty(PropertyName = "username")]

        public string Username { get; set; }
        [JsonProperty(PropertyName = "password")]

        public string Password { get; set; }
        [JsonProperty(PropertyName = "currentSection")]

        public int CurrentSection { get; set; }
        [JsonProperty(PropertyName = "currentSeat")]

        public int Seat { get; set; }
        [JsonProperty(PropertyName = "orders")]

        public Order[] Orders { get; set; }
        [JsonProperty(PropertyName = "isEmployee")]

        public int IsEmployee { get; set; }
    }
}
