using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models
{
    public class NolowaImage
    {
        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; } = string.Empty;

        [JsonProperty("hash")]
        public string Hash { get; set; } = string.Empty;
    }
}
