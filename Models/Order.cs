﻿using Newtonsoft.Json;
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


        public string UserId { get; set; }
        [JsonProperty(PropertyName = "concessionId")]

        public string ConcessionId { get; set; }
        [JsonProperty(PropertyName = "isCartOrder")]

        public int IsCartOrder { get; set; }
        [JsonProperty(PropertyName = "isCompleted")]

        public int IsCompleted { get; set; }
        [JsonProperty(PropertyName = "items")]

        public Item[] Items { get; set; }
        [JsonProperty(PropertyName ="timeCompleted")]
        public DateTime TimeCompleted { get; set; }
        [JsonProperty(PropertyName ="orderedSection")]
        public int OrderedSection { get; set; }
        [JsonProperty(PropertyName = "orderedSeat")]
        public int OrderedSeat { get; set; }
    }
}
