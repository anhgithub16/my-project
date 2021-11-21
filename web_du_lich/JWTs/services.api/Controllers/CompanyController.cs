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
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _companyService;
        private ILogger<CompanyController> _iLogger;
        public CompanyController(ICompanyService companyService,ILogger<CompanyController> iLogger)
        {
            _companyService = companyService;
            _iLogger = iLogger;
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _companyService.GetAll();
            return Ok(rs);
        }
        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetById(string id)
        {
            var rs = _companyService.GetById(id);
            return Ok(rs);
        }
        [HttpPost]
        [Route("getallbypaging")]
        public IActionResult GetAllByPaging(PagingItem pagingItem)
        {
            var rs = _companyService.GetAllByPaging(pagingItem);
            return Ok(rs);
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string keyword)
        {
            var rs = _companyService.Search(keyword);
            return Ok(rs);
        }
        [HttpPost]
        [Route("save")]
        public IActionResult Save(Company company)
        {
            string userId = this.GetUserId();
            var rs = _companyService.Save(company, userId);
            return Ok(rs);
        }
        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(string id)
        {
            string userId = this.GetUserId();
            var rs = _companyService.Delete(id, userId);
            return Ok(rs);
        }
    }
}
