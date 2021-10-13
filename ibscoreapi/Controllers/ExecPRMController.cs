using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IbsCoreapi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ibscoreapi.Controllers
{
    
    [ApiController]
    public class ExecPRMController : ControllerBase
    {

        [HttpGet]
        [Route("fcwberp/Service.asmx/ExecPRM")]
        public IActionResult Create([FromQuery(Name = "email")] string _email, [FromQuery(Name = "passWord")] string _passWord, [FromQuery(Name = "usr")] string _usr, [FromQuery(Name = "siTe")] string _siTe)
        {

            DBManager dbManager = new DBManager();

            Mailp comParam = new Mailp();
            comParam.email = _email;
            comParam.passWord = _passWord;
            comParam.usr = _usr;
            comParam.siTe = _siTe;

            string json = "";
            try
            {


               json = dbManager.ExecPRM(comParam);


            }
            catch (Exception ex)
            {
                json = ex.Message;
            }
            return new JsonResult(json); //CreatedAtRoute("GetTodo", new { id = 0 }, json);
        }
    }
}
