﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ResourceServer.Controllers
{
    [Authorize]
    public class TestController : ApiController
    {
        [Route("api/test/me")]
        public string Get()
        {
            return this.User.Identity.Name;
        }
    }
}
