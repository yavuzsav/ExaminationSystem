using ExaminationSystem.Business.Abstract;
using ExaminationSystem.MvcCoreWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExaminationSystem.MvcCoreWebUI.Controllers
{
    public class ExamController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public ExamController(IQuestionService questionService, ICategoryService categoryService, IUserService userService)
        {
            _questionService = questionService;
            _categoryService = categoryService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetList().Data;

            return View(categories);
        }

        public IActionResult GetExam(string categoryId)
        {
            var questions = _questionService.GetExam(categoryId);

            if (questions.Success)
            {
                var model = new ExamViewModel
                {
                    Questions = questions.Data
                };

                return View(model);
            }

            return BadRequest(questions.Message);
        }

        [HttpPost]
        public IActionResult FinishExam(ExamViewModel examViewModel)
        {
            var user = _userService.GetUserByUserName(User.Identity.Name).Data;
            var result = _questionService.FinishExam(examViewModel.Questions.Select(x => x.Id).ToList(), examViewModel.UserAnswers, user).Data; //todo user

            return View(result);
        }
    }
}