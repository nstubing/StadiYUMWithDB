using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentDBTodo.Models
{
    public class Order
    {
        [JsonProperty(PropertyName = "id")]

        public string Id { get; set; }
        [JsonProperty(PropertyName = "userId")]


        public int UserId { get; set; }
        [JsonProperty(PropertyName = "concession")]

        public Concession Concession { get; set; }
        [JsonProperty(PropertyName = "isCartOrder")]

        public bool IsCartOrder { get; set; }
        [JsonProperty(PropertyName = "isCompleted")]

        public bool IsCompleted { get; set; }
        [JsonProperty(PropertyName = "items")]

        public Item[] Items { get; set; }
    }
}
