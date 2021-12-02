using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.Models
{
    public class User
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Adress address { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public Company company { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as User;
            return obj == null || 
                GetType() != obj.GetType() || 
                !(obj is User) || Id != other.Id && Name != other.Name && UserName != other.UserName && 
                Email != other.Email && address != other.address && Phone != other.Phone && WebSite != other.WebSite &&
                company != other.company ? false : true;
        }
    }
}
