using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using services.svc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace services.api.Controllers
{
    [Route("api/combo")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboService _comboService;
        public ComboController(IComboService comboService)
        {
            _comboService = comboService;
        }
        [HttpGet]
        [Route("getbytourdetailid")]
        public IActionResult GetByTourDetailId(string tourDetailId)
        {
            var rs = _comboService.GetByTourDetailId(tourDetailId);
            return Ok(rs);
        }
    }
}
