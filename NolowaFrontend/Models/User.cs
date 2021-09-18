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
        public string Name { get; set; } = string.Empty;

        [JsonProperty("joinDate")]
        public DateTime JoinDate { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("followIds")]
        public List<long> FollowIds { get; set; } = new List<long>();

        [JsonProperty("profileImage")]
        public byte[] ProfileImage { get; set; }
    }
}
