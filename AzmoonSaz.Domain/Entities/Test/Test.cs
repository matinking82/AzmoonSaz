using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Domain.Entities.Test
{
    public record Test
    {
        public Test()
        {

        }

        public int Id { get; set; }

        public int ClassId { get; set; }
        public virtual Classroom.Classroom Classroom { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Question> Questions { get; set; }
    }
}
