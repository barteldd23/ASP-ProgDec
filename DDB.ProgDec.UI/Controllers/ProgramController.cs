using DDB.ProgDec.UI.Models;
using DDB.ProgDec.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DDB.ProgDec.UI.Controllers
{
    public class ProgramController : Controller
    {
        private readonly IWebHostEnvironment _host;

        public ProgramController(IWebHostEnvironment host)
        {
            _host = host;
        }


        #region "Pre-WebAPI"
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

            // allow access to multiple models in the view :
            ProgramVM programVM = new ProgramVM();
            programVM.Program = new BL.Models.Program();
            programVM.DegreeTypes = DegreeTypeManager.Load();

            if (Authenticate.IsAuthenticated(HttpContext))
                return View(programVM);
            else
                return RedirectToAction("Login", "User", new {returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request)});
            
            
        }

        [HttpPost]
        public IActionResult Create(ProgramVM programVM, bool rollback = false)
        {
            try
            {
                int result = ProgramManager.Insert(programVM.Program, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                // allow access to multiple models in the view :
                ProgramVM programVM = new ProgramVM();
                programVM.Program = ProgramManager.LoadByID(id);
                programVM.DegreeTypes = DegreeTypeManager.Load();

                ViewBag.Title = "Edit " + programVM.Program.Description;

                return View(programVM);
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        [HttpPost]
        public IActionResult Edit(ProgramVM programVM, bool rollback = false)
        {
            try
            {
                ProcessImage(programVM);

                int result = ProgramManager.Update(programVM.Program, rollback);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(programVM);
            }
        }

        private void ProcessImage(ProgramVM programVM)
        {
            // process the image
            if (programVM.File != null)
            {
                programVM.Program.ImagePath = programVM.File.FileName;

                string path = _host.WebRootPath + "\\images\\";

                using (var stream = System.IO.File.Create(path + programVM.File.FileName))
                {
                    programVM.File.CopyTo(stream);
                    ViewBag.Message = "File Uploaded Successfully...";
                }
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
        #endregion

        #region Web-API

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7130/api/");
            return client;
        }

        public IActionResult Get()
        {
            ViewBag.Title = "List of All Programs";
            HttpClient client = InitializeClient();

            // Call the API
            HttpResponseMessage response = client.GetAsync("Program").Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            List<BL.Models.Program> programs = items.ToObject<List<BL.Models.Program>>();

            // calls the view from the Index Method and passes the programs variable.
            return View(nameof(Index), programs) ;
        }

        public IActionResult GetOne(int id)
        {
            ViewBag.Title = "Progam Details";
            HttpClient client = InitializeClient();

            // Call the API
            HttpResponseMessage response = client.GetAsync("Program/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            BL.Models.Program program = item.ToObject<BL.Models.Program>();

            return View(nameof(Details), program);
        }

        public IActionResult Insert()
        {
            ViewBag.Title = "Create";
            HttpClient client = InitializeClient();

            HttpResponseMessage response = client.GetAsync("DegreeType").Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            List<DegreeType> degreeTypes = items.ToObject<List<DegreeType>>();

            ProgramVM programVM = new ProgramVM();
            programVM.DegreeTypes = degreeTypes;
            programVM.Program = new BL.Models.Program();

            return View(nameof(Create), programVM);

        }

        [HttpPost]
        public IActionResult Insert(ProgramVM programVM)
        {
            try
            {
                HttpClient client = InitializeClient();

                // Method we stole from Edit Post above
                ProcessImage(programVM);

                string serializedObject = JsonConvert.SerializeObject(programVM.Program);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Call the API
                HttpResponseMessage response = client.PostAsync("Program", content).Result;

                return RedirectToAction(nameof(Get));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Create), programVM);
            }
        }

        public IActionResult Update(int id)
        {
            ViewBag.Title = "Update";
            HttpClient client = InitializeClient();

            // Call the API For One Program, coppied from GetOne method
            HttpResponseMessage response = client.GetAsync("Program/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            BL.Models.Program program = item.ToObject<BL.Models.Program>();

            // Call API For List of DegreeTypes we need to send to the view
            response = client.GetAsync("DegreeType").Result;

            // Parse the result
            result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            List<DegreeType> degreeTypes = items.ToObject<List<DegreeType>>();

            ProgramVM programVM = new ProgramVM();
            programVM.DegreeTypes = degreeTypes;
            programVM.Program = program;

            return View(nameof(Edit), programVM);

        }

        [HttpPost]
        public IActionResult Update(int id, ProgramVM programVM)
        {
            try
            {
                HttpClient client = InitializeClient();

                // Method we stole from Edit Post above
                ProcessImage(programVM);

                string serializedObject = JsonConvert.SerializeObject(programVM.Program);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Call the API CHANGE TO PutAsync FOR UPDATE FUNCTIONALITY NOT INSERT
                HttpResponseMessage response = client.PutAsync("Program/" + id, content).Result;

                return RedirectToAction(nameof(Get));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Edit), programVM);
            }
        }

        public IActionResult Remove(int id)
        {
            HttpClient client = InitializeClient();

            // Call the API For One Program, coppied from GetOne method
            HttpResponseMessage response = client.GetAsync("Program/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            BL.Models.Program program = item.ToObject<BL.Models.Program>();

            return View(nameof(Delete), program);
        }

        [HttpPost]
        public IActionResult Remove(int id, BL.Models.Program program)
        {
            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Program/" + id).Result;
                return RedirectToAction(nameof(Get));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Delete), program);

            }
        }




        #endregion
    }
}
