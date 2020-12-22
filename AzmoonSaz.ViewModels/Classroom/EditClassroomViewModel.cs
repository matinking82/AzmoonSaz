﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.ViewModels.Classroom
{
    public class EditClassroomViewModel
    {
        [Display(Name ="نام جدید")]
        public string Name { get; set; }

        public int ClassId { get; set; }
    }
}
