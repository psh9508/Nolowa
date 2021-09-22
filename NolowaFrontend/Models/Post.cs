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
        public string Message { get; set; } = string.Empty;

        [JsonProperty("id")]
        public long ID { get; set; }

        [JsonProperty("userId")]
        public string UserID { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("uploadedDate")]
        public DateTime UploadedDate { get; set; }
    }
}
