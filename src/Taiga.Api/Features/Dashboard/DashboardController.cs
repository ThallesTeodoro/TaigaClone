using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Taiga.Core.Entities;
using Taiga.Api.Extensions;
using Taiga.Core.Interfaces;
using Taiga.Core.Interfaces.ServicesInterfaces;
using Microsoft.Extensions.Configuration;
using Taiga.Core.Services;
using Taiga.Api.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace Taiga.Api.Features.Dashboard
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Json("Ok");
        }
    }
}
