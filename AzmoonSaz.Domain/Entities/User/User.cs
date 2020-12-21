using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Domain.Entities.User
{
    public record User
    {
        public User()
        {

        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        public virtual IEnumerable<UserInClassroom> UserInClassrooms { get; set; }
        public virtual IEnumerable<Classroom.Classroom> Classrooms { get; set; }
    }
}
