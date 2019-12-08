using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Models.Dtos.User;
using ExaminationSystem.MvcCoreWebUI.HelperMethods;
using ExaminationSystem.MvcCoreWebUI.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.MvcCoreWebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IAuthService authService, IUserService userService, IRoleService roleService)
        {
            _authService = authService;
            _userService = userService;
            _roleService = roleService;
        }

        public IActionResult Index(string searchString = null, int page = 1)
        {
            int pageSize = 50;
            var list = _userService.GetAll().Data;
            var searchedList = list.Where(x => searchString != null &&
                                               (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                x.UserName.ToLower().Contains(searchString.ToLower())
                                               )).ToList();
            List<string> usersRoles = searchedList.Count == 0 ?
                list.Select(user => _roleService.GetRoleNamesByUser(user).Data).ToList()
                : searchedList.Select(user => _roleService.GetRoleNamesByUser(user).Data).ToList();

            UserListViewModel model = new UserListViewModel
            {
                Users = PagingListSelectBySearch.SelectList(list, searchedList, page, pageSize),
                UsersRoles = usersRoles,
                PageCount = PagingListSelectBySearch.GetPageCount(list, searchedList, pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };

            ViewBag.title = "Tüm Kullanıcılar";
            return View(model);
        }

        public IActionResult Admins(string searchString = null, int page = 1)
        {
            int pageSize = 50;
            var list = _roleService.GetUsersInRoleName("Admin").Data;

            var searchedList = list.Where(x => searchString != null &&
                                               (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                x.UserName.ToLower().Contains(searchString.ToLower())
                                                )).ToList();
            //var searchedList = list.Where(x => search != null && x.CategoryName.ToLower().Contains(search.ToLower())).ToList();

            UserListViewModel model = new UserListViewModel
            {
                Users = PagingListSelectBySearch.SelectList(list, searchedList, page, pageSize),
                PageCount = PagingListSelectBySearch.GetPageCount(list, searchedList, pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };

            ViewBag.title = "Yöneticiler";
            return View("Index", model);
        }

        public IActionResult Students(string searchString = null, int page = 1)
        {
            int pageSize = 50;
            var list = _roleService.GetUsersInRoleName("Student").Data;
            var searchedList = list.Where(x => searchString != null &&
                                               (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                x.UserName.ToLower().Contains(searchString.ToLower())
                                               )).ToList();

            UserListViewModel model = new UserListViewModel
            {
                Users = PagingListSelectBySearch.SelectList(list, searchedList, page, pageSize),
                PageCount = PagingListSelectBySearch.GetPageCount(list, searchedList, pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };

            ViewBag.title = "Öğrenciler";
            return View("Index", model);
        }

        public IActionResult RoleAssign(string userId)
        {
            var user = _userService.GetByUserId(userId).Data;
            if (user != null)
            {
                var roleNames = _roleService.GetAllRoleNames().Data;

                ViewBag.roles = new SelectList(roleNames);
                RoleAssignViewModel model = new RoleAssignViewModel
                {
                    UserName = user.UserName
                };
                return View(model);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult RoleAssign(RoleAssignViewModel roleAssignViewModel)
        {
            var user = _userService.GetByUserName(roleAssignViewModel.UserName).Data;

            if (user != null)
            {
                var result = _roleService.RoleAssign(user, roleAssignViewModel.RoleName);

                if (result.Success)
                    return RedirectToAction("Index", "User");
                else
                    return BadRequest();
            }

            return View(roleAssignViewModel);
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
                    var roles = _roleService.GetRoleNamesByUser(user.Data).Data;

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home");
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