using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using NolowaFrontend.Models.Images;

namespace NolowaFrontend.Models
{
    public class User
    {
        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("accountId")]
        public string AccountID { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("joinDate")]
        public DateTime JoinDate { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("followers")]
        public List<followerUser> Followers { get; set; } = new List<followerUser>();

        [JsonProperty("profileImage")]
        public ProfileImage ProfileImage { get; set; } = new ProfileImage();
    }
}
