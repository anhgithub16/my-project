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
    [Route("api/appparam")]
    [ApiController]
    public class AppParamController : ControllerBase
    {
        private readonly IAppParamService _appParamService;
        private readonly ILogger<AppParamController> _iLogger;
        public AppParamController (IAppParamService appParamService, ILogger<AppParamController> ilogger)
        {
            _appParamService = appParamService;
            _iLogger = ilogger;
        }
        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetById(string id)
        {
            var rs = _appParamService.GetById(id);
            return Ok(rs);
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _appParamService.GetAll();
            return Ok(rs);
        }
        [HttpPost]
        [Route("getallbypaging")]
        public IActionResult GetAllByPaging(PagingItem pagingItem)
        {
            var rs = _appParamService.GetAllByPaging(pagingItem);
            return Ok(rs);
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var rs = _appParamService.Search(keyword);
            return Ok(rs);
        }
        [HttpPost]
        [Route("save")]
        public IActionResult Save(AppParam appParam)
        {
            string userId = this.GetUserId();
            var rs = _appParamService.Save(appParam, userId);
            return Ok(rs);
        }
        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(string id)
        {
            string userId = this.GetUserId();
            var rs = _appParamService.Delete(id, userId);
            return Ok(rs);
        }
    }
}
