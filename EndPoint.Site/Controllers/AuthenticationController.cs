using AzmoonSaz.Application.Services.Interfaces;
using AzmoonSaz.Common.DTOs.User;
using AzmoonSaz.Common.Enums;
using AzmoonSaz.Common.Utilities;
using AzmoonSaz.ViewModels.User;
using EndPoint.Site.Utilities;
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

        #region Signup
        [HttpGet]
        [Route("/Signup")]
        public IActionResult Signup()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

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

            if (result.Status != ServiceStatus.Success)
            {
                ModelState.AddModelError(nameof(UserSignupViewModel.UserName), result.Message);
                return View(user);
            }

            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion


        #region Login

        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            RequestLoginUserDto request = new RequestLoginUserDto()
            {
                UserName = user.UserName,
                Password = user.Password
            };

            var result = await _userServices.LoginUserAsync(request);

            if (result.Status != ServiceStatus.Success)
            {

                ModelState.AddModelError(nameof(UserLoginViewModel.UserName), result.Message);
                return View(user);
            }

            await HttpContext.LoginToSiteAsync(result.Data, user.UserName);

            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion

        #region Signout
        [HttpGet]
        [Route("/Signout")]
        public async Task<IActionResult> Signout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignoutSiteAsync();
            }

            return Redirect("/");
        }

        #endregion
    }
}
