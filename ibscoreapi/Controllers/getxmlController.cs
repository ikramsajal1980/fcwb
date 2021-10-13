using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IbsCoreapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getxmlController : ControllerBase
    {

        //public List<Calculation> CalculateWithPost(CalculationInputs inputs)

        [HttpPost]
        public IActionResult Create([FromBody] commp comParam)
        {

            DBManager dbManager = new DBManager();
            string json = "";
            try
            {
               

                    json = dbManager.writexml(comParam);

                
            }
            catch (Exception ex)
            {
                json = ex.Message;
            }
            return new JsonResult(json); //CreatedAtRoute("GetTodo", new { id = 0 }, json);
        }
    }
}
