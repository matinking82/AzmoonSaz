using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Common.DTOs.Classroom
{
    public record RequestCreateClassroomByUserDto
    {
        public string ClassName { get; set; }
        public int CreatorId { get; set; }
    }
}
