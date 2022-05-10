using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models.IF
{
    class IFFollowModel
    {
        [JsonProperty("source_id")]
        public long SourceID { get; set; }
        [JsonProperty("dest_id")]
        public long DestID { get; set; }
    }
}
