using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace NolowaFrontend.Models
{
    public class User
    {
        [JsonProperty("id")]
        public long ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("joinDate")]
        public DateTime JoinDate { get; set; }
    }
}
