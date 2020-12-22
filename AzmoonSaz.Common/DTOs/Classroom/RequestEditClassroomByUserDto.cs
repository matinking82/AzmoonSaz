using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Common.DTOs.Classroom
{
    public record RequestEditClassroomByUserDto
    {
        public int ClassId { get; set; }
        public string NewName { get; set; }
    }
}
