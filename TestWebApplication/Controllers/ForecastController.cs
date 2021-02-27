using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForecastController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
