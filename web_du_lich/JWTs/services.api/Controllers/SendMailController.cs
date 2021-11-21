using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using services.svc.Models;
using services.svc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace services.api.Controllers
{
    [Route("api/sendmail")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        public readonly ISendMailServices _sendMailServices;
        public SendMailController(ISendMailServices sendMailServices)
        {
            _sendMailServices = sendMailServices;
        }
        [HttpPost]
        [Route("sendmail")]
        public IActionResult SendMail(MailContent mailContent)
        {
            _sendMailServices.SendMail(mailContent);
            return Ok();
        }
    }
}
