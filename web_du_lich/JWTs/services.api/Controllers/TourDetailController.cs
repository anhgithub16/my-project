using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using services.svc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace services.api.Controllers
{
    [Route("api/tourdetail")]
    [ApiController]
    public class TourDetailController : ControllerBase
    {
        private readonly ITourDetailService _tourDetailService;
        public TourDetailController(ITourDetailService tourDetailService)
        {
            _tourDetailService = tourDetailService;
        }
        [HttpGet]
        [Route("getbycityid")]
        public IActionResult GetByCityId(int cityId)
        {
            var rs = _tourDetailService.GetByCityId(cityId);
            return Ok(rs);
        }
        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetById(string id)
        {
            var rs = _tourDetailService.GetById(id);
            return Ok(rs);
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var rs = _tourDetailService.Search(keyword);
            return Ok(rs);
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _tourDetailService.GetAll();
            return Ok(rs);
        }
    }
}
