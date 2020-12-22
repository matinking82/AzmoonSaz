using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.ViewModels.User
{
    public class AddStudentByTeacherViewModel
    {
        [Display(Name ="نام کربری")]
        public string UserName { get; set; }
        public int ClassId { get; set; }
    }
}
