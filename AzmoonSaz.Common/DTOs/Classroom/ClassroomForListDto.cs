using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Common.DTOs.Classroom
{
    public record ClassroomForListDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public bool IsMyClass { get; set; }
    }
}
