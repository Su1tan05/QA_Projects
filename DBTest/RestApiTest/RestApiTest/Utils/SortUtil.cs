using RestApiTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.Utils
{
    public static class SortUtil
    {
        public static bool isSortByAscending(List<Post> posts)
        {
            if (posts.SequenceEqual(posts.OrderBy(x => x.Id).ToList()))
                return true;
            else
                return false;
        }
    }
}
