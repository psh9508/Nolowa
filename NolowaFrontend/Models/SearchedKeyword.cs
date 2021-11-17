using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models
{
    public class SearchedKeyword
    {
        [JsonProperty("keyword")]
        public string Keyword { get; set; } = string.Empty;
    }
}
