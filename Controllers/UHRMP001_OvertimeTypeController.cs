using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PowerERP.Controllers
{
    [Route("[controller]")]
    public class UHRMP001_OvertimeTypeController : Controller
    {
        private readonly ILogger<UHRMP001_OvertimeTypeController> _logger;

        public UHRMP001_OvertimeTypeController(ILogger<UHRMP001_OvertimeTypeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}