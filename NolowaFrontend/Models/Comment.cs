using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NolowaFrontend.Models
{
    public class Comment
    {
        public Image ProfileImage { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Likes{ get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
