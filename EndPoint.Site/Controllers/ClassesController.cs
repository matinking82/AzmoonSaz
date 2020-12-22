using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.DTOs.Classroom;
using AzmoonSaz.Common.Enums;
using AzmoonSaz.ViewModels.Classroom;
using AzmoonSaz.ViewModels.User;
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

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateClassroomViewModel classroom)
        {
            if (!ModelState.IsValid)
            {
                return View(classroom);
            }
            var UserId = User.GetUserId();
            RequestCreateClassroomByUserDto request = new RequestCreateClassroomByUserDto()
            {
                ClassName = classroom.ClassName,
                CreatorId = UserId
            };

            var result = await _classroomService.CreateClassroomByUser(request);

            if (result.Status != ServiceStatus.Success)
            {
                ModelState.AddModelError(nameof(CreateClassroomViewModel.ClassName), result.Message);
                return View(classroom);
            }


            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _classroomService.GetClassroomDetailForEditByUser(Id);

            if (result.Status != ServiceStatus.Success)
            {
                ViewBag.Message = result.Message;

                return RedirectToAction(nameof(Index));
            }
            var model = new EditClassroomViewModel()
            {
                ClassId = result.Data.ClassId,
                Name = result.Data.NewName,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditClassroomViewModel edit)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }

            RequestEditClassroomByUserDto request = new RequestEditClassroomByUserDto()
            {
                ClassId = edit.ClassId,
                NewName = edit.Name
            };

            var result = await _classroomService.EditClassroomByUser(request);

            if (result.Status != ServiceStatus.Success)
            {
                ModelState.AddModelError(nameof(EditClassroomViewModel.Name), result.Message);
                return View(edit);
            }


            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Details

        public async Task<IActionResult> Details(int Id = 0)
        {
            if (Id == 0)
            {
                return NotFound();
            }

            var result = await _classroomService.GetStudentsListForClassByClassId(Id);

            if (result.Status != ServiceStatus.Success)
            {
                ViewBag.Message = result.Message;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClassName = (await _classroomService.GetClassroomByClassId(Id)).ClassName;

            return View(result.Data
                .Select(s => new StudentsListForClass()
                {
                    UserId = s.UserId,
                    UserName = s.UserName,
                }));
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int Id = 0)
        {
            if (Id == 0)
            {
                return NotFound();
            }

            var result = await _classroomService.DeleteClassroomByClassId(Id);

            if (result.Status != ServiceStatus.Success)
            {
                ViewBag.Message = result.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
