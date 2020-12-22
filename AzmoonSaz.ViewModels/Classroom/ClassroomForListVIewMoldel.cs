using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.ViewModels.Classroom
{
    public record ClassroomForListVIewMoldel
    {
        public int ClassId { get; set; }
        [Display(Name ="نام کلاس")]
        public string ClassName { get; set; }
        [Display(Name ="کلاس من؟")]
        public bool IsMyClass { get; set; }
    }
}
