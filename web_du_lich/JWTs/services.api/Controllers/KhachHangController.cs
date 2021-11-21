using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using services.svc.Entities;
using services.svc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace services.api.Controllers
{
    [Route("api/khachhang")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangService _khachHangService;
        public KhachHangController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(KhachHang khachHang)
        {
            var rs = _khachHangService.Insert(khachHang);
            return Ok(rs);
        }
        [HttpGet]
        [Route("searchkh")]
        public IActionResult SearchKh(string keyword)
        {
            var rs = _khachHangService.SearchKh(keyword);
            return Ok(rs);
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _khachHangService.GetAll();
            return Ok(rs);
        }
    }
}
