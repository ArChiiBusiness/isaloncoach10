using BLL.Interfaces;
using BOL;
using isaloncoach10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace isaloncoach10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFormService _formService;
        private IStatisticsService _statisticsService;

        public HomeController(ILogger<HomeController> logger, IFormService formService, IStatisticsService statisticsService)
        {
            _formService = formService;
            _statisticsService = statisticsService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _statisticsService.GetStatistics());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
