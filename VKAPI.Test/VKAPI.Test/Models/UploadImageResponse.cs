using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Test.Models
{
    public class UploadImageResponse
    {
        [JsonProperty("response")]
        public UploadImage UploadImage { get; set; }
    }
}
