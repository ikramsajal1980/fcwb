using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IbsCoreapi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Download(dFileName dfile)
        {

           
            try
            {
               // var path = @"fcwberp\DataFile\ss.xml";
               var path = @"fcwberp/DataFile/" + dfile.dfileName + "";
                var memory = new MemoryStream();
                using (var streem = new FileStream(path, FileMode.Open))
                {
                    await streem.CopyToAsync(memory);
                }
                memory.Position = 0;
                var ext = Path.GetExtension(path).ToLowerInvariant();
                return File(memory,"bin/bin", Path.GetFileName(path));
            }
            catch
            {
                throw;
            }
        }

      



        
        
    }
}
