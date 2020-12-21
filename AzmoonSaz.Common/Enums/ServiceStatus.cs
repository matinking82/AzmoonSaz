using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Common.Enums
{
    public enum ServiceStatus
    {
        [Display(Name ="با موفقیت انجام شد")]
        Success,

        [Display(Name ="مشکلی در سیستم پیش آمد")]
        SystemError,

        [Display(Name ="یافت نشد")]
        NotFound,

        [Display(Name ="ورودی ها اشتباه است")]
        InputParametersError,

        [Display(Name ="دسترسی ندارید")]
        AccessDenied,

        [Display(Name ="مشکلی در ذخیره فایل پیش آمد")]
        SaveFileError,

        [Display(Name ="خطایی رخ داد")]
        Error
    }
}
