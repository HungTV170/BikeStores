using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BikesApp.Controllers.API
{
    public class abcController : ApiController
    {
        private readonly ILog _log;
        public abcController(ILog log)
        {
            _log = log;
        }
        public string get()
        {
            return _log.Log("Hung");
        }
    }

    public interface ILog
    {
        string Log(string message);
    }

    public class hellolog : ILog
    {
        public string Log(string message)
        {
            return "Hello " + message;
        }
    }
}
