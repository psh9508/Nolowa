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
    }
}
