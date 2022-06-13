using Newtonsoft.Json;
using NolowaFrontend.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models
{
    public class ProfileInfo
    {
        public long Id { get; set; }

        public ProfileImage ProfileImage { get; set; } = new ProfileImage();
        
        public ProfileImage BackgroundImage { get; set; } = new ProfileImage();
        
        public string Message { get; set; } = string.Empty;
    }
}
