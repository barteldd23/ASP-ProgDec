﻿using Microsoft.AspNetCore.Mvc;

namespace DDB.ProgDec.UI.Controllers
{
    public class DegreeTypeController : Controller
    {
        public IActionResult Index()
        {
            return View(DegreeTypeManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(DegreeTypeManager.LoadByID(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int result = DegreeTypeManager.Insert(degreeType, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            return View(DegreeTypeManager.LoadByID(id));
        }

        [HttpPost]
        public IActionResult Edit(DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int result = DegreeTypeManager.Update(degreeType, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(degreeType);
            }
        }

        public IActionResult Delete(int id)
        {
            return View(DegreeTypeManager.LoadByID(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int result = DegreeTypeManager.Delete(id, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(degreeType);
            }
        }
    }
}