using Newtonsoft.Json;
using NolowaFrontend.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NolowaFrontend.Models
{
    public class Post
    {
        public long USN { get; set; }
        public int Likes { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Image> Contents { get; set; } = new List<Image>();

        public string Message { get; set; } = string.Empty;

        public long FollowId { get; set; }

        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("insertDate")]
        public DateTime UploadedDateTime { get; set; }

        public User PostedUser { get; set; } = new User();

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid Guid { get; set; }
    }
}
