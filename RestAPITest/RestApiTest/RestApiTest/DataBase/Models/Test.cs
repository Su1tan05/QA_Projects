using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.DataBase.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public string MethodName { get; set; }
        public int ProjectId { get; set; }
        public int SessionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Env { get; set; }
        public string Browser { get; set; }
        public int ?AutorId { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Test;
            return obj == null ||
                GetType() != obj.GetType() ||
                !(obj is Test) || (Id != other.Id || Name != other.Name || StatusId != other.StatusId ||
                MethodName != other.MethodName || ProjectId != other.ProjectId || SessionId != other.SessionId || StartTime.ToString() != other.StartTime.ToString() ||
                EndTime.ToString() != other.EndTime.ToString() || Env !=other.Env || Browser != other.Browser || AutorId != other.AutorId) ? false : true;
        }
    }
}
