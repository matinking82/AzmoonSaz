using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.ViewModels.Classroom
{
    public class CreateClassroomViewModel
    {
        [Display(Name ="نام کلاس")]
        public string ClassName { get; set; }
    }
}
