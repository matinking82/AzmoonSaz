using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.DTOs.User;
using AzmoonSaz.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserServices _userServices;
        public AuthenticationController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        [Route("/Signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [Route("/Signup")]
        public async Task<IActionResult> Signup(UserSignupViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            RequestSignupUserDto request = new RequestSignupUserDto() 
            { 
                Password = user.Password,
                UserName = user.UserName
            };

            var result = await _userServices.SignupUserAsync(request);

            ViewBag.IsSuccess = true;

            return View();
        }
    }
}
