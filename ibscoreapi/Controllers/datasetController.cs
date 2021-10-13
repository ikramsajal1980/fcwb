using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IbsCoreapi.Controllers
{

    ////// aip/dataset

    [Route("api/[controller]")]
    [ApiController]
    public class datasetController : ControllerBase
    {

      
        //public List<Calculation> CalculateWithPost(CalculationInputs inputs)

        [HttpPost]
        public string  CalculateWithPost([FromBody] Common comParam)
     //   public IActionResult Create([FromBody] Common comParam)
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
                    
                        json = dbManager.ExecuteQuery(comParam);
                   
                }
            }
            catch (Exception ex)
            {
                json = ex.Message;
            }
            return json; //CreatedAtRoute("GetTodo", new { id = 0 }, json);
        }


    }
}
