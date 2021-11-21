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
    [Route("api/employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _empService;
        private readonly ILogger<EmployeesController> _ilogger;
        public EmployeesController(IEmployeeService empService,ILogger<EmployeesController> ilogger)
        {
            _empService = empService;
            _ilogger = ilogger;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _empService.GetAll();
            return Ok(rs);
        }
        [HttpPost]
        [Route("getallbypaging")]
        public IActionResult GetAllByPaging(PagingItem pagingItem)
        {
            var rs = _empService.GetAllByPaging(pagingItem);
            return Ok(rs);
        }
        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetById(string id)
        {
            var rs = _empService.GetById(id);
            return Ok(rs);
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var rs = _empService.Search(keyword);
            return Ok(rs);
        }
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(Employees employees)
        {
            var userId = this.GetUserId();
            var rs = _empService.Insert(employees,userId);
            return Ok(rs);
        }
        [HttpPost]
        [Route("update")]
        public IActionResult Update(Employees employees)
        {
            var userId = this.GetUserId();
            var rs = _empService.Update(employees,userId);
            return Ok(rs);
        }
        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(string id)
        {
            var useId = this.GetUserId();
            var rs = _empService.Delete(id,useId);
            return Ok(rs);
        }
    }
}
