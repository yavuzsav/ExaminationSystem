using ExaminationSystem.Business.Abstract;
using ExaminationSystem.MvcCoreWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ExaminationSystem.Models.IdentityEntities;

namespace ExaminationSystem.MvcCoreWebUI.Controllers
{
    public class ExamController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ICategoryService _categoryService;

        public ExamController(IQuestionService questionService, ICategoryService categoryService)
        {
            _questionService = questionService;
            _categoryService = categoryService;
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
            var result = _questionService.FinishExam(examViewModel.Questions.Select(x => x.Id).ToList(), examViewModel.UserAnswers, new AppUser()).Data; //todo user

            return View(result);
        }
    }
}