using Newtonsoft.Json;
using NolowaFrontend.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NolowaFrontend.Models
{
    public class Post
    {
        [JsonProperty("postId")]
        public long PostID { get; set; }
        public int Likes { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Image> Contents { get; set; } = new List<Image>();

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("followId")]
        public long FollowId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("uploadedDateTime")]
        public DateTime UploadedDateTime { get; set; }

        [JsonProperty("postedUser")]
        public User PostedUser { get; set; } = new User();

        [JsonIgnore]
        public Guid Guid { get; set; }
    }
}
