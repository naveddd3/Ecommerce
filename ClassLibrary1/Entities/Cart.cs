using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("items")]
        public Dictionary<string, Item> Items { get; set; }
    }
    public class Item
    {
        [JsonProperty("product")]
        public CartProductReq Product { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }

}

