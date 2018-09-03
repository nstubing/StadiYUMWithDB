using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentDBTodo.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]

        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]


        public string Name { get; set; }
        [JsonProperty(PropertyName = "price")]


        public double Price { get; set; }
        [JsonProperty(PropertyName = "imgstring")]


        public string Image { get; set; }
        [JsonProperty(PropertyName = "concessionName")]


        public string ConcessionName { get; set; }
        [JsonProperty(PropertyName = "isInStock")]

        public int IsInStock { get; set; }
    }
}
