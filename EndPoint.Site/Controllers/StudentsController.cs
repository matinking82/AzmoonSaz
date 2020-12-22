using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.DTOs.User;
using AzmoonSaz.Common.Enums;
using AzmoonSaz.ViewModels.User;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IClassroomService _classroomService;

        public StudentsController(IUserServices userServices, IClassroomService classroomService)
        {
            _classroomService = classroomService;
            _userServices = userServices;
        }

        #region Create
        [HttpGet]
        //Id = ClassId
        public async Task<IActionResult> Create(int Id)
        {
            var UserId = User.GetUserId();

            var classroom = await _classroomService.GetClassroomByClassId(Id);

            if (classroom.CreatorId != UserId)
            {
                ViewBag.Message = "شما نمیتوانید به این کلاس دانش آموزی اضافه کنید";
                return RedirectToAction("Index", "Home");
            }

            return View(new AddStudentByTeacherViewModel()
            {
                ClassId = Id
            });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(AddStudentByTeacherViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            RequestAddStudentByTeacherDto request = new RequestAddStudentByTeacherDto()
            {
                ClassId = user.ClassId,
                UserName = user.UserName,
            };
            var result = await _userServices.AddStudentByTeacher(request);

            if (result.Status != ServiceStatus.Success)
            {
                ModelState.AddModelError(nameof(AddStudentByTeacherViewModel.UserName), result.Message);
                return View(user);
            }

            return RedirectToAction(nameof(ClassesController.Details), "Classes", new { Id = user.ClassId });
        }
        #endregion


        #region Delete
        
        [HttpGet]
        public async Task<IActionResult> Delete(int Id=0)
        {
            if (Id==0)
            {
                return NotFound();
            }

            //TODO: Delete User

            return RedirectToAction(nameof(ClassesController.Index), "Classes");
        }

        #endregion
    }
}
