using Newtonsoft.Json;
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
        public Image ProfileImage { get; set; }
        public int Likes { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Image> Contents { get; set; } = new List<Image>();

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("postId")]
        public long PostID { get; set; }

        [JsonProperty("userId")]
        public long UserID { get; set; }

        [JsonProperty("userAccountId")]
        public string UserAccountId { get; set; } = string.Empty;

        [JsonProperty("followId")]
        public long FollowId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("uploadedDate")]
        public DateTime UploadedDate { get; set; }
    }
}
