using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IbsCoreapi
{

    public class commp
    {
        public string Key { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ServiceName { get; set; }
        public string Query { get; set; }
        public string FileName { get; set; }

    }


    public class dFileName
    {
    
        public string dfileName { get; set; }

    }
}


