using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Security;
using services.svc.Entities;
using services.svc.Models;
using services.svc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace services.api.Controllers
{
    [Route("api/holiday")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _iHolidayService;
        private readonly ILogger<HolidayController> _iLogger;
        public HolidayController(IHolidayService iHolidayService, ILogger<HolidayController> iLogger)
        {
            _iHolidayService = iHolidayService;
            _iLogger = iLogger;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _iHolidayService.GetAll();
            return Ok(rs);
        }
        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetById(string id)
        {
            var rs = _iHolidayService.GetById(id);
            return Ok(rs);
        }
        [HttpPost]
        [Route("getallbypaging")]
        public IActionResult GetAllByPaging(PagingItem pagingItem)
        {
            var rs = _iHolidayService.GetAllByPaging(pagingItem);
            return Ok(rs);
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var rs = _iHolidayService.Search(keyword);
            return Ok(rs);
        }
        [HttpPost]
        [Route("save")]
        public IActionResult Save(Holiday holiday)
        {
            string userId = "hta";
            var rs = _iHolidayService.Save(holiday, userId);
            return Ok(rs);
        }
        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(string id)
        {
            string userId = this.GetUserId();
            var rs = _iHolidayService.Delete(id, userId);
            return Ok(rs);
        }
    }
}
