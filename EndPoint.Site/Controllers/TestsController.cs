using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.ViewModels.Tests;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class TestsController : Controller
    {
        private readonly ITestService _testService;
        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        public async Task<IActionResult> Index()
        {

            int UserId = User.GetUserId();

            var result = await _testService.GetTestsForUserByUserId(UserId);

            return View(result.Data.Select(c => new TestsClassViewModel()
            {
                ClassId = c.ClassId,
                ClassName = c.ClassName,
                IsMyClass = c.IsMyClass,
                Tests = c.Tests.Select(t => new TestsListViewModel()
                {
                    Description = t.Description,
                    Id = t.Id,
                    QuestionsCount = t.QuestionsCount,
                    Title = t.Title
                }).ToList()
            }));
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int i)
        {
            return View();
        }
    }
}
