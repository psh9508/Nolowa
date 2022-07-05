using Newtonsoft.Json;
using NolowaFrontend.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NolowaFrontend.Models
{
    public class SearchedUser
    {
        [JsonProperty("user_id")]
        public long ID { get; set; }

        [JsonPropertyName("accountName")]
        public string Name { get; set; }

        public string UserId { get; set; }

        public ProfileInfo ProfileInfo { get; set; } = new ProfileInfo();
    }
}
