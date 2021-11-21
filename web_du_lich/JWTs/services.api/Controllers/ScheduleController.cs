using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using services.svc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace services.api.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _scheduleService.GetAll();
            return Ok(rs);
        }
        [HttpGet]
        [Route("getbyday")]
        public IActionResult GetByDay(string tripId,int day)
        {
            var rs = _scheduleService.GetByDay(tripId,day);
            return Ok(rs);
        }
    }
}
