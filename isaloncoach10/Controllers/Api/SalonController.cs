using BOL;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace isaloncoach10.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private string ApiKey = "0ca36de3-6603-4e32-ad2c-7ab85aa703aa";
        private ISalonService _salonService;

        public SalonController(ISalonService salonService)
        {
            _salonService = salonService;
        }

        [Route("actuals")]
        public async Task<IActionResult> Actuals([FromQuery] string apikey)
        {
            if (apikey != ApiKey)
            {
                return Unauthorized();
            }
            return new JsonResult(await _salonService.GetAllActuals());
        }
    }
}
