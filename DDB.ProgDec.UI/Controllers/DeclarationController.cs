using Microsoft.AspNetCore.Mvc;

namespace DDB.ProgDec.UI.Controllers
{
    public class DeclarationController : Controller
    {
        public IActionResult Index()
        {
            return View(DeclarationManager.Load());
        }

        // filter the declaration by programId
        public IActionResult Browse(int id)
        {
            return View(nameof(Index),DeclarationManager.Load(id));
        }

        public IActionResult Details(int id)
        {
            return View(DeclarationManager.LoadByID(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Declaration declaration, bool rollback = false)
        {
            try
            {
                int result = DeclarationManager.Insert(declaration, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            return View(DeclarationManager.LoadByID(id));
        }

        [HttpPost]
        public IActionResult Edit(Declaration declaration, bool rollback = false)
        {
            try
            {
                int result = DeclarationManager.Update(declaration, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(declaration);
            }
        }

        public IActionResult Delete(int id)
        {
            return View(DeclarationManager.LoadByID(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Declaration declaration, bool rollback = false)
        {
            try
            {
                int result = DeclarationManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(declaration);
            }
        }
    }
}
