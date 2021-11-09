using Newtonsoft.Json;
using NolowaFrontend.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models
{
    public class SearchedUser
    {
        [JsonProperty("user_id")]
        public long ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("accountId")]
        public string AccountID { get; set; }

        [JsonProperty("profileImage")]
        public ProfileImage ProfileImage { get; set; } = new ProfileImage();
    }
}
