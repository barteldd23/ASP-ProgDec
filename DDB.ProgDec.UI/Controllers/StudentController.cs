using Microsoft.AspNetCore.Mvc;

namespace DDB.ProgDec.UI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View(StudentManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(StudentManager.LoadByID(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student, bool rollback = false)
        {
            try
            {
                int result = StudentManager.Insert(student, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            return View(StudentManager.LoadByID(id));
        }

        [HttpPost]
        public IActionResult Edit(Student student, bool rollback = false)
        {
            try
            {
                int result = StudentManager.Update(student, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }

        public IActionResult Delete(int id)
        {
            return View(StudentManager.LoadByID(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Student student, bool rollback = false)
        {
            try
            {
                int result = StudentManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }
    }
}
