using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Test.Models
{
    public class Image
    {
        [JsonProperty("server")]
        public string Server { get; set; }
        [JsonProperty("photo")]
        public string Photo { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}
