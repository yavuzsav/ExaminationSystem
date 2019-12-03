using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Models.Dtos.User;
using ExaminationSystem.MvcCoreWebUI.HelperMethods;
using ExaminationSystem.MvcCoreWebUI.ViewModels.Notes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ExaminationSystem.MvcCoreWebUI.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public NoteController(INoteService noteService, IUserService userService, ICategoryService categoryService)
        {
            _noteService = noteService;
            _userService = userService;
            _categoryService = categoryService;
        }

        public IActionResult GetAll(int page = 1)
        {
            int pageSize = 30;
            var list = _noteService.GetAll().Data;

            List<UserDto> users = new List<UserDto>();

            foreach (var note in list)
            {
                users.Add(_userService.GetByUserId(note.UserId).Data);
            }

            NoteListViewModel model = new NoteListViewModel
            {
                Notes = PagingListSelectBySearch.SelectList(list, null, page, pageSize),
                Users = users,
                PageCount = PagingListSelectBySearch.GetPageCount(list, null, pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };

            return View(model);
        }

        public IActionResult GetAllByUser(int page = 1)
        {
            int pageSize = 30;
            var user = _userService.GetUserByUserName(User.Identity.Name).Data;
            var list = _noteService.GetByUserId(user.Id).Data;

            NoteListViewModel model = new NoteListViewModel
            {
                Notes = PagingListSelectBySearch.SelectList(list, null, page, pageSize),
                PageCount = PagingListSelectBySearch.GetPageCount(list, null, pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };

            return View("GetAll", model);
        }
    }
}