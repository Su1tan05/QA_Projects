using RestApiTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.Data
{
    public class TestData
    {
        public User User { get; set; }
        public int CustomUserId { get; set; }
        public int[] NumberOfPost { get; set; }
        public int UserId { get; set; }
    }
}
