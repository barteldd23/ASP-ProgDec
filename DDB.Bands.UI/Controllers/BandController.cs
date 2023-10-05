using DDB.Bands.UI.Extensions;
using DDB.Bands.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDB.Bands.UI.Controllers
{
    public class BandController : Controller
    {

        Band[] bands;

        public void LoadBands()
        {
            bands = new Band[]
            {
                new Band{Id = 1, Name="Eric Clapton", Genre="Blues", DateFounded = new DateTime(1978,4,3)},
                new Band{Id = 2, Name="Linkin Park", Genre="Alternative Rock", DateFounded = new DateTime(1996,2,15)},
                new Band{Id = 3, Name="Disturbed", Genre="Rock", DateFounded = new DateTime(1994,10,13)}
            };
        }

        private void GetBands()
        {
            if (HttpContext.Session.GetObject<Band[]>("bands") != null)
            {
                bands = HttpContext.Session.GetObject<Band[]>("bands");
            }else
            {
                LoadBands();
            }
        }

        private void SetBands()
        {
            HttpContext.Session.SetObject("bands", bands);
        }

        // GET: BandController
        public ActionResult Index()
        {
            GetBands();
            return View(bands);
        }

        // GET: BandController/Details/5
        public ActionResult Details(int id)
        {
            GetBands();
            Band band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        // GET: BandController/Create
        // goes to the screen
        public ActionResult Create()
        {
            Band band = new Band();
            return View(band);
        }

        // POST: BandController/Create
        // Sends data out to POST and actually create a new object
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Band band) // this was the band object sent from the form when submit button pressed
        {
            try
            {
                GetBands();

                // Resize the array
                Array.Resize(ref bands, bands.Length + 1 );

                // Calculate the new id value
                band.Id = bands.Length;
                bands[bands.Length - 1] = band;

                SetBands();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BandController/Edit/5
        public ActionResult Edit(int id)
        {
            GetBands();
            Band band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        // POST: BandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Band band)
        {
            try
            {
                GetBands();
                bands[id - 1] = band;
                SetBands();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BandController/Delete/5
        public ActionResult Delete(int id)
        {
            GetBands();
            Band band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        // POST: BandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                GetBands();
                var newbands = bands.Where(b => b.Id != id);
                bands = newbands.ToArray();

                SetBands();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
