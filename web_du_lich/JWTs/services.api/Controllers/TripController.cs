using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using services.svc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace services.api.Controllers
{
    [Route("api/trip")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _tripService.GetAll();
            return Ok(rs);
        }
        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetById(string id)
        {
            var rs = _tripService.GetById(id);
            return Ok(rs);
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var rs = _tripService.Search(keyword);
            return Ok(rs);
        }
    }
}
