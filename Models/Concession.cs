using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentDBTodo.Models
{
    public class Concession
    {
        [JsonProperty(PropertyName = "id")]

        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]

        public string Name { get; set; }
        [JsonProperty(PropertyName = "section")]

        public int Section { get; set; }
        [JsonProperty(PropertyName = "image")]

        public string Image { get; set; }
        [JsonProperty(PropertyName = "items")]

        public Item[] Items { get; set; }
    }
}
