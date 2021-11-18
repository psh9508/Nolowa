using Newtonsoft.Json;
using NolowaFrontend.Core;
using NolowaFrontend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models
{
    public class NolowaImage
    {
        // It must be checked later.
        //[JsonProperty("id")]
        //public long ID { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; } = string.Empty;

        [JsonProperty("hash")]
        public string Hash { get; set; } = string.Empty;

        public string ProfileImageFile => this.Hash.IsNotVaild() ? Constant.DEFAULT_PROFILE_IMAGE_FULL_PATH : Constant.PROFILE_IMAGE_ROOT_PATH + Hash + ".jpg";
    }
}
