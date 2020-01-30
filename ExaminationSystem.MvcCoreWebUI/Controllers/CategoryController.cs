using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Models.Entities;
using ExaminationSystem.MvcCoreWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExaminationSystem.MvcCoreWebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IClassLevelService _classLevelService;
        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        private string CurrentUserName => User.Identity.Name ?? "Anonymous";

        public CategoryController(ICategoryService categoryService, IClassLevelService classLevelService, IQuestionService questionService)
        {
            _categoryService = categoryService;
            _classLevelService = classLevelService;
            _questionService = questionService;
        }

        public IActionResult Index()
        {
            var list = _categoryService.GetList().Data;

            CategoryListViewModel model = new CategoryListViewModel
            {
                Categories = list
            };

            return View(model);
        }

        public IActionResult GetQuestions(string id)
        {
            var questions = _questionService.GetByCategoryId(id).Data;

            if (questions == null)
                return NotFound();

            return View(questions);
        }

        public IActionResult Add()
        {
            ViewBag.ClassLevel = new SelectList(_classLevelService.GetAll().Data, "Id", "ClassLevelName");

            return View();
        }

        [HttpPost]
        public IActionResult Add([Bind("CategoryName,ClassLevelId")] Category category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Add(category, CurrentUserName);

                if (result.Success)
                {
                    ViewBag.ClassLevel = new SelectList(_classLevelService.GetAll().Data, "Id", "ClassLevelName");
                    return RedirectToAction("Index", "Category");
                }
                ModelState.AddModelError("", result.Message);
            }

            ViewBag.ClassLevel = new SelectList(_classLevelService.GetAll().Data, "Id", "ClassLevelName");
            return View(category);
        }

        public IActionResult Update(string id)
        {
            var category = _categoryService.GetById(id);
            ViewBag.ClassLevel = new SelectList(_classLevelService.GetAll().Data, "Id", "ClassLevelName");

            if (category.Data == null)
            {
                return BadRequest();
            }

            return View(category.Data);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                var current = _categoryService.GetById(category.Id).Data;

                if (current == null)
                    return BadRequest();

                current.CategoryName = category.CategoryName;
                current.ClassLevelId = category.ClassLevelId;

                var result = _categoryService.Update(current, CurrentUserName);

                if (result.Success)
                {
                    ViewBag.ClassLevel = new SelectList(_classLevelService.GetAll().Data, "Id", "ClassLevelName");
                    return RedirectToAction("Index", "Category");
                }
            }

            ViewBag.ClassLevel = new SelectList(_classLevelService.GetAll().Data, "Id", "ClassLevelName");
            return View();
        }

        public IActionResult Delete(string id)
        {
            var category = _categoryService.GetById(id);
            if (category.Data == null)
                ModelState.AddModelError("", category.Message);

            var result = _categoryService.Delete(category.Data);

            if (result.Success)
            {
                TempData["result"] = "success";
                return RedirectToAction("Index", "Category");
            }

            TempData["result"] = "error";
            return RedirectToAction("Index", "Category");
        }
    }
}