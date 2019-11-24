using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MvcCoreWebUI.Controllers
{
    public class ExamParameterController : Controller
    {
        private readonly IExamParameterService _examParameterService;

        public ExamParameterController(IExamParameterService examParameterService)
        {
            _examParameterService = examParameterService;
        }

        public IActionResult Index()
        {
            var parameter = _examParameterService.GetParameter().Data;

            return View(parameter);
        }

        public IActionResult ExamParameterAssign()
        {
            var parameter = _examParameterService.GetParameter().Data;

            if (parameter != null)
                return View("Index", parameter);

            return BadRequest();
        }

        [HttpPost]
        public IActionResult ExamParameterAssign(ExamParameter examParameter)
        {
            var result = _examParameterService.AddOrUpdate(examParameter);

            if (result.Success)
                return RedirectToAction("Index", "ExamParameter");

            return BadRequest();
        }
    }
}