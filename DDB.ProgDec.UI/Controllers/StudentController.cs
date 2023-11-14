using Microsoft.AspNetCore.Mvc;

namespace DDB.ProgDec.UI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of Advisors";
            return View(StudentManager.Load());
        }

        public IActionResult Details(int id)
        {
            var item = StudentManager.LoadByID(id);
            ViewBag.Title = "Details for " + item.FullName;
            return View(item);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create a Student";
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
            catch (Exception ex)
            {
                ViewBag.Title = "Create a Student";
                ViewBag.Error = ex.Message;
                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            var item = StudentManager.LoadByID(id);
            ViewBag.Title = "Details for " + item.FullName;
            return View(item);
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
                ViewBag.Title = "Details for " + student.FullName;
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }

        public IActionResult Delete(int id)
        {
            var item = StudentManager.LoadByID(id);
            ViewBag.Title = "Are you sure you want to Delete " + item.FullName;
            return View(item);
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
                ViewBag.Title = "Are you sure you want to Delete " + student.FullName;
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }
    }
}
