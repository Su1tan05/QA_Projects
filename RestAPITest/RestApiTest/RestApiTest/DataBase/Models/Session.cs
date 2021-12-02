using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.DataBase.Models
{
    public class Session
    {
        public int id { get; set; }
        public int SessionKey { get; set; }
        public DateTime CreatedTime { get; set; }
        public int BuildNumber { get; set; }
    }
}
