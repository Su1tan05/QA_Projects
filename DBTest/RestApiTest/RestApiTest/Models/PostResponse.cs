using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.Models
{
    public class PostResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<Post> Posts { get; set; }
        public Post Post { get; set; }
    }
}
