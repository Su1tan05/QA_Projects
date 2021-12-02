using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Test.Models.SaveImageModel
{
    public class SaveImageResponse
    {
        [JsonProperty("response")]
        public List<SaveImage> SaveImage { get; set; }
    }
}
