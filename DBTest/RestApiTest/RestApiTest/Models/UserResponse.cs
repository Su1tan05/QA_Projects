using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace RestApiTest.Models
{
    public class UserResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<User> Users { get; set; }
        public User User { get; set; }
    }
}
