using DDB.ProgDec.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DDB.ProgDec.BL.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace DDB.ProgDec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<BL.Models.Student> Get()
        {
            return StudentManager.Load();
        }

        // tell it here in the httpget that there is a parameter
        [HttpGet("{id}")]
        public BL.Models.Student Get(int id)
        {
            return StudentManager.LoadByID(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BL.Models.Student dstudent)
        {
            try
            {
                int results = StudentManager.Insert(dstudent);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put( int id, [FromBody] BL.Models.Student dstudent)
        {
            try
            {
                int results = StudentManager.Update(dstudent);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int results = StudentManager.Delete(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }


        
    
}
