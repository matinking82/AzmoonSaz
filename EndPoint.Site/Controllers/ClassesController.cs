using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.Enums;
using AzmoonSaz.ViewModels.Classroom;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassroomService _classroomService;
        public ClassesController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }


        public async Task<IActionResult> Index()
        {
            var UserId = User.GetUserId();

            var result = await _classroomService.GetUsersClassroomsByUserId(UserId);

            if (result.Status != ServiceStatus.Success)
            {
                ViewBag.Message = result.Message;
                return Redirect("/");
            }


            return View(result.Data
                .Select(c => new ClassroomForListVIewMoldel()
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    IsMyClass = c.IsMyClass
                }));
        }
    }
}
