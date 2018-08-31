using Microsoft.Azure.Documents.Spatial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentDBTodo.Models
{
    public class Stadium
    {
        [JsonProperty(PropertyName = "id")]

        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]

        public string Name { get; set; }
        [JsonProperty(PropertyName = "street")]

        public string Street { get; set; }
        [JsonProperty(PropertyName = "city")]

        public string City { get; set; }
        [JsonProperty(PropertyName = "zipcode")]

        public int Zipcode { get; set; }
        [JsonProperty(PropertyName = "state")]

        public string State { get; set; }
        [JsonProperty(PropertyName = "image")]

        public string Image { get; set; }
        [JsonProperty(PropertyName = "location")]

        public Point Location {get;set;}
        [JsonProperty(PropertyName = "isOpen")]

        public int IsOpen { get; set; }
    }
}
