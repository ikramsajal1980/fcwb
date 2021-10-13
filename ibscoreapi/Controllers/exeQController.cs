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
    public class exeQController : ControllerBase
    {

        [HttpPost]
        public IActionResult Create([FromBody] Common comParam)
        {

            DBManager dbManager = new DBManager();
            string json = "";
            try
            {
                if (comParam.Key != "API")
                {
                    json = "You are not permitted";
                }
                else
                {

                    json = dbManager.ExecuteNonQuery(comParam);

                }
            }
            catch (Exception ex)
            {
                json = ex.Message;
            }
            return new JsonResult(json); //CreatedAtRoute("GetTodo", new { id = 0 }, json);
        }
    }
}
