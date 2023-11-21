using DDB.ProgDec.PL;
using DDB.ProgDec.UI.ViewModels;
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
            StudentVM studentVM = new StudentVM(id);
            ViewBag.Title = "Details for " + studentVM.Student.FullName;
            HttpContext.Session.SetObject("advisorids", studentVM.AdvisorIds);
            return View(studentVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, StudentVM studentVM, bool rollback = false)
        {
            try
            {

                IEnumerable<int> newAdvisorIds = new List<int>();
                if(studentVM.AdvisorIds != null)
                {
                    newAdvisorIds = studentVM.AdvisorIds;
                }

                IEnumerable<int> oldAdvisorIds = new List<int>();
                oldAdvisorIds = GetObject(); // function we made below

                IEnumerable<int> deletes = oldAdvisorIds.Except(newAdvisorIds);
                IEnumerable<int> adds = newAdvisorIds.Except(oldAdvisorIds);

                deletes.ToList().ForEach(d => StudentAdvisorManager.Delete(id, d));
                adds.ToList().ForEach(a => StudentAdvisorManager.Insert(id, a));

                int result = StudentManager.Update(studentVM.Student, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Details for " + studentVM.Student.FullName;
                ViewBag.Error = ex.Message;
                return View(studentVM);
            }
        }

        private IEnumerable<int> GetObject()
        {
            if (HttpContext.Session.GetObject<IEnumerable<int>>("advisorids") != null)
            {
                return (HttpContext.Session.GetObject<IEnumerable<int>>("advisorids");
            }
            else
            {
                return null;
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
