using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTask.Models
{
    public class TestInfo
    {
        public string SessionId { get; set; }
        public string ProjectName { get; set; }
        public string TestName{ get; set; }
        public string MethodName { get; set; }
        public string Env { get; set; }
        public string BrowserName { get; set; }
    }
}
