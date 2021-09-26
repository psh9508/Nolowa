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
        [JsonProperty("profileImage")]
        public NolowaImage ProfileImage { get; set; } = new NolowaImage();

        [JsonProperty("userId")]
        public long UserID { get; set; }
    }
}
