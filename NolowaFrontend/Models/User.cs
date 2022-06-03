using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using NolowaFrontend.Models.Images;
using NolowaFrontend.Core;
using NolowaFrontend.Extensions;

namespace NolowaFrontend.Models
{
    public class User
    {
        [JsonProperty("id")]
        public long ID { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;

        [JsonProperty("joinDate")]
        public DateTime JoinDate { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("followers")]
        public List<Follower> Followers { get; set; } = new List<Follower>();

        public ProfileInfo ProfileInfo { get; set; } = new ProfileInfo();

        [JsonProperty("jwtToken")]
        public string JWTToken { get; set; } = string.Empty;

        public string ProfileImageFile => Constant.PROFILE_IMAGE_ROOT_PATH + ProfileInfo.ProfileImage.Hash + ".jpg";
    }
}
