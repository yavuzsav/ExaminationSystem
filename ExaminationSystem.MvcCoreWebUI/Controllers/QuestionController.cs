using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Models.Entities;
using ExaminationSystem.MvcCoreWebUI.HelperMethods;
using ExaminationSystem.MvcCoreWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ExaminationSystem.MvcCoreWebUI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ICategoryService _categoryService;
        private string CurrentUserName => User.Identity.Name ?? "Anonymous";

        public QuestionController(IQuestionService questionService, ICategoryService categoryService)
        {
            _questionService = questionService;
            _categoryService = categoryService;
        }

        public IActionResult Index(string search = null, int page = 1)
        {
            int pageSize = 30;
            var questions = _questionService.GetAll().Data;
            var searchedList = questions.Where(x => search != null && x.QuestionContent.ToLower().Contains(search.ToLower())).ToList();

            QuestionListViewModel model = new QuestionListViewModel
            {
                Questions = PagingListSelectBySearch.SelectList(questions, searchedList, page, pageSize),
                PageCount = PagingListSelectBySearch.GetPageCount(questions, searchedList, pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };

            if (string.IsNullOrWhiteSpace(search))
            {
                return View(model);
            }

            return View(model);
        }

        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetList().Data, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Question question)
        {
            ViewBag.Categories = new SelectList(_categoryService.GetList().Data, "Id", "CategoryName");
            if (ModelState.IsValid)
            {
                var result = _questionService.AddQuestion(question, CurrentUserName);

                if (!result.Success)
                    return BadRequest(result.Message);

                return RedirectToAction("Index", "Question");
            }
            return View(question);
        }

        public ActionResult Update(string id)
        {
            ViewBag.Categories = new SelectList(_categoryService.GetList().Data, "Id", "CategoryName");

            var result = _questionService.GetById(id);

            if (result.Data == null)
            {
                return BadRequest(result.Message);
            }

            return View(result.Data);
        }

        [HttpPost]
        public ActionResult Update(Question question)
        {
            if (ModelState.IsValid)
            {
                var current = _questionService.GetById(question.Id).Data;

                if (current != null)
                {
                    current.QuestionContent = question.QuestionContent;
                    current.A = question.A;
                    current.B = question.B;
                    current.C = question.C;
                    current.D = question.D;
                    current.CorrectAnswer = question.CorrectAnswer;
                    current.CategoryId = question.CategoryId;

                    var result = _questionService.UpdateQuestion(current, CurrentUserName);

                    if (result.Success)
                        return RedirectToAction("Index", "Question");
                    else
                        return BadRequest(result.Message);
                }
            }

            return View(question);
        }

        public ActionResult Delete(string id)
        {
            var category = _questionService.GetById(id);

            if (category.Data == null)
                return BadRequest();

            var result = _questionService.DeleteQuestion(category.Data);

            if (result.Success)
                return RedirectToAction("Index", "Question");

            return BadRequest();
        }
    }
}