﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Web.Controllers.Tests
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
