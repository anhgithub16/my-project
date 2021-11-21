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
    [Authorize]
    [Route("api/feedback")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService _fbService;
        private readonly ILogger<FeedBackController> _ilogger;
        public FeedBackController(IFeedBackService fbService, ILogger<FeedBackController> ilogger)
        {
            _fbService = fbService;
            _ilogger = ilogger;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _fbService.GetAll();
            return Ok(rs);
        }
        [HttpPost]
        [Route("getallbypaging")]
        public IActionResult GetAllByPaging(PagingItem pagingItem)
        {
            var rs = _fbService.GetAllByPaging(pagingItem);
            return Ok(rs);
        }
        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetById(string id)
        {
            var rs = _fbService.GetById(id);
            return Ok(rs);
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var rs = _fbService.Search(keyword);
            return Ok(rs);
        }
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(FeedBacks feedBacks)
        {
            var userId = this.GetUserId();
            var rs = _fbService.Insert(feedBacks, userId);
            return Ok(rs);
        }
        [HttpPost]
        [Route("update")]
        public IActionResult Update(FeedBacks feedBacks)
        {
            var userId = this.GetUserId();
            var rs = _fbService.Update(feedBacks, userId);
            return Ok(rs);
        }
        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(string id)
        {
            var useId = this.GetUserId();
            var rs = _fbService.Delete(id, useId);
            return Ok(rs);
        }
    }
}
