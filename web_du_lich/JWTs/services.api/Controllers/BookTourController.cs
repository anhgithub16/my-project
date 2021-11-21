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
    [Route("api/booktour")]
    [ApiController]
    public class BookTourController : ControllerBase
    {
        private readonly IBookTourService _bookTourService;
        public BookTourController(IBookTourService bookTourService)
        {
            _bookTourService = bookTourService;
        }
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(BookTour bookTour)
        {
            var rs = _bookTourService.Insert(bookTour);
            return Ok(rs);
        }
        [HttpGet]
        [Route("getalltrip")]
        public IActionResult GetAllTrip()
        {
            var rs = _bookTourService.GetAllTrip();
            return Ok(rs);
        }
        [HttpGet]
        [Route("getallhotel")]
        public IActionResult GetAllHotel()
        {
            var rs = _bookTourService.GetAllHotel();
            return Ok(rs);
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var rs = _bookTourService.GetAll();
            return Ok(rs);
        }
    }
}
