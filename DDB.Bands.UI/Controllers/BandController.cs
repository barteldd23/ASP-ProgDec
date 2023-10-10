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
                new Band{Id = 1, Image="https://www.bing.com/images/search?view=detailV2&ccid=VLFaTy41&id=72DBA76CAF9C5F13D4773B5C8BA5DCC0D0D4A65B&thid=OIP.VLFaTy41cQHfQH_X-90EUAHaKY&mediaurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.54b15a4f2e357101df407fd7fbdd0450%3frik%3dW6bU0MDcpYtcOw%26riu%3dhttp%253a%252f%252fupload.wikimedia.org%252fwikipedia%252fcommons%252f1%252f1f%252fEric-Clapton_1975.jpg%26ehk%3d5saj2b5c4dBccrk0UzafbmW0hKbai1ti8g6z%252f4E1RnI%253d%26risl%3d%26pid%3dImgRaw%26r%3d0&exph=1024&expw=730&q=eric+clapton+band&simid=608010736541922053&FORM=IRPRST&ck=B4683166774B7A26C7A1E7B9BAE124B5&selectedIndex=33",Name="Eric Clapton", Genre="Blues", DateFounded = new DateTime(1978,4,3)},
                new Band{Id = 2, Image="https://www.google.com/imgres?imgurl=https%3A%2F%2Fwww.azcentral.com%2Fgcdn%2F-mm-%2F4e09c2618c2c46c8dd6669ce088d1637a1ad52a5%2Fc%3D0-17-648-382%2Flocal%2F-%2Fmedia%2FPhoenix%2FPhoenix%2F2014%2F05%2F27%2F%2F1401210370000-linkin-park.jpg%3Fwidth%3D1200%26disable%3Dupscale%26format%3Dpjpg%26auto%3Dwebp&tbnid=GwEvioYOchHBlM&vet=12ahUKEwiLwsjDkeyBAxX-8ckDHbDfD5EQMygBegQIARBp..i&imgrefurl=https%3A%2F%2Fwww.azcentral.com%2Fstory%2Fentertainment%2Fmusic%2F2014%2F05%2F29%2Flinkin-park-sublime-rome-beef-vegan%2F9730635%2F&docid=p8sk1KUc9avSqM&w=648&h=365&q=linkin%20park%20band&ved=2ahUKEwiLwsjDkeyBAxX-8ckDHbDfD5EQMygBegQIARBp",Name="Linkin Park", Genre="Alternative Rock", DateFounded = new DateTime(1996,2,15)},
                new Band{Id = 3, Image="https://www.bing.com/images/search?view=detailV2&ccid=fXKuvCmI&id=03834A7056DD923C30FCA4A07AED7AF283FA0559&thid=OIP.fXKuvCmIQDUmxycgE2CT-QHaD4&mediaurl=https%3a%2f%2fwww.guitartricks.com%2fimg%2fartist_banner%2fdisturbed.jpg&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.7d72aebc2988403526c72720136093f9%3frik%3dWQX6g%252fJ67XqgpA%26pid%3dImgRaw%26r%3d0&exph=628&expw=1200&q=Disturbed++band&simid=608029960822811134&FORM=IRPRST&ck=7580E6BB4A68F64AA2D5BFF9C007FBE3&selectedIndex=17",
                                 Name="Disturbed", Genre="Rock", DateFounded = new DateTime(1994,10,13)}
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
            return View("IndexCard",bands);
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
