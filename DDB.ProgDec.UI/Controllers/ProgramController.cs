using Microsoft.AspNetCore.Mvc;

namespace DDB.ProgDec.UI.Controllers
{
    public class ProgramController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of All Programs";
            return View(ProgramManager.Load());
        }

        public IActionResult Details(int id)
        {
            var item = ProgramManager.LoadByID(id);
            ViewBag.Title = "Details for " + item.Description;
            return View(item);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create a Progarm";
            return View();
        }

        [HttpPost]
        public IActionResult Create(BL.Models.Program program, bool rollback = false)
        {
            try
            {
                int result = ProgramManager.Insert(program, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            var item = ProgramManager.LoadByID(id);
            ViewBag.Title = "Edit " + item.Description;
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(BL.Models.Program program, bool rollback = false)
        {
            try
            {
                int result = ProgramManager.Update(program, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(program);
            }
        }

        public IActionResult Delete(int id)
        {
            var item = ProgramManager.LoadByID(id);
            ViewBag.Title = "Delete";
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(int id, BL.Models.Program program, bool rollback = false)
        {
            try
            {
                int result = ProgramManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(program);
            }
        }
    }
}
