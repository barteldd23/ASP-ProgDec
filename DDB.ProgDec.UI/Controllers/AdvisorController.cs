using Microsoft.AspNetCore.Mvc;

namespace DDB.ProgDec.UI.Controllers
{
    public class AdvisorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of Advisors";
            return View(AdvisorManager.Load());
        }

        public IActionResult Details(int id)
        {
            var item = AdvisorManager.LoadByID(id);
            ViewBag.Title = "Details for " + item.Name;
            return View(AdvisorManager.LoadByID(id));
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create an Advisor";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Advisor advisor, bool rollback = false)
        {
            try
            {
                int result = AdvisorManager.Insert(advisor, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Create an Advisor";
                ViewBag.Error = ex.Message;
                return View(advisor);
            }
        }

        public IActionResult Edit(int id)
        {
            var item = AdvisorManager.LoadByID(id);
            ViewBag.Title = "Edit " + item.Name;
            return View(AdvisorManager.LoadByID(id));
        }

        [HttpPost]
        public IActionResult Edit(Advisor advisor, bool rollback = false)
        {
            try
            {
                int result = AdvisorManager.Update(advisor, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit " + advisor.Name;
                ViewBag.Error = ex.Message;
                return View(advisor);
            }
        }

        public IActionResult Delete(int id)
        {
            var item = AdvisorManager.LoadByID(id);
            ViewBag.Title = "Delete " + item.Name;
            return View(AdvisorManager.LoadByID(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Advisor advisor, bool rollback = false)
        {
            try
            {
                int result = AdvisorManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete " + advisor.Name;
                ViewBag.Error = ex.Message;
                return View(advisor);
            }
        }
    }
}
