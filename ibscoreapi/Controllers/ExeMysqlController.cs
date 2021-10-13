using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IbsCoreapi;


namespace ibscoreapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExeMysqlController : Controller
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

                    json = dbManager.ExecuteNonQueryMySql(comParam);

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
