using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Models.Entities;
using ExaminationSystem.MvcCoreWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MvcCoreWebUI.Controllers
{
    public class ClassLevelController : Controller
    {
        private readonly IClassLevelService _classLevelService;

        public ClassLevelController(IClassLevelService classLevelService)
        {
            _classLevelService = classLevelService;
        }

        public IActionResult Index()
        {
            var list = _classLevelService.GetAll().Data;         

            ClassLevelListViewModel model = new ClassLevelListViewModel
            {
                ClassLevels = list
            };

            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ClassLevel classLevel)
        {
            if (ModelState.IsValid)
            {
                var result = _classLevelService.Add(classLevel);

                if (result.Success)
                {
                    return RedirectToAction("Index", "ClassLevel");
                }
            }

            return View(classLevel);
        }

        public IActionResult Update(string id)
        {
            var result = _classLevelService.GetById(id).Data;

            if (result == null)
            {
                return BadRequest();
            }

            return View(result);
        }

        [HttpPost]
        public IActionResult Update(ClassLevel classLevel)
        {
            if (ModelState.IsValid)
            {
                var current = _classLevelService.GetById(classLevel.Id).Data;

                if (current == null)
                {
                    return BadRequest();
                }

                current.ClassLevelName = classLevel.ClassLevelName;
                current.Description = classLevel.Description;

                var result = _classLevelService.Update(current);

                if (result.Success)
                {
                    return RedirectToAction("Index", "ClassLevel");
                }
            }

            return View(classLevel);
        }

        public IActionResult Delete(string id)
        {
            var classLevel = _classLevelService.GetById(id).Data;

            if (classLevel == null)
            {
                return BadRequest();
            }

            var result = _classLevelService.Delete(classLevel);

            if (result.Success)
            {
                return RedirectToAction("Index", "ClassLevel");
            }

            return RedirectToAction("Index", "ClassLevel");
        }
    }
}