using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoadSaintsAPI.Controllers
{
    [RoutePrefix("api/beng")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("world")]
        public IHttpActionResult Oka() { 
            return Ok("Hello, World!");
        }

        [HttpGet]
        [Route("bal")]

        public IHttpActionResult Bal()
        {
            return Ok("Heda");
        }
    }
}
