using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MvcCoreWebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UserController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            if (ModelState.IsValid)
            {
                var result = _authService.SignIn(userForLoginDto);

                if (result.Success)
                {
                    var user = _userService.GetByMail(userForLoginDto.Email);
                    var roles = _userService.GetRoleNames(user.Data).Data;

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (roles.Contains("Student"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            return View(userForLoginDto);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var result = _authService.SignUp(userForRegisterDto);
                if (result.Success)
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }

            return View(userForRegisterDto);
        }

        public void SignOut()
        {
            _authService.SignOut();
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }
    }
}