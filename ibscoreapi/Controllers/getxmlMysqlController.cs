using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IbsCoreapi;

namespace ibscoreapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getxmlMysqlController : ControllerBase
    {


        [HttpPost]
        public IActionResult Create([FromBody] commp comParam)
        {

            DBManager dbManager = new DBManager();
            string json = "";
            try
            {


                json = dbManager.writexmlmySql(comParam);


            }
            catch (Exception ex)
            {
                json = ex.Message;
            }
            return new JsonResult(json); //CreatedAtRoute("GetTodo", new { id = 0 }, json);
        }
    }
}
