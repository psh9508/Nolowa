using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models
{
    public class followerUser
    {
        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("profileImage")]
        public NolowaImage ProfileImage { get; set; } = new NolowaImage();
    }
}
