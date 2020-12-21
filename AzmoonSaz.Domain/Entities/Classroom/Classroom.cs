using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Domain.Entities.Classroom
{
    public record Classroom
    {
        public Classroom()
        {

        }

        public int Id { get; set; }
        
        public int CreatorId { get; set; }
        public User.User Creator { get; set; }

        public string ClassName { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual IEnumerable<User.UserInClassroom> UserInClassrooms { get; set; }
        public virtual IEnumerable<Test.Test> Tests { get; set; }
    }
}
