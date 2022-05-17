using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models
{
    public class Follower
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("destinationAccountId")]
        public long DestinationAccountId { get; set; }

        [JsonProperty("sourceAccountId")]
        public long SourceAccountId { get; set; }
    }
}
