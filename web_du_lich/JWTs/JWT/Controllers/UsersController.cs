using JWT.Entity;
using JWT.Model;
using JWT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUserService _iuserService;
        public UsersController(IUserService iuserService)
        {
            _iuserService = iuserService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Users users)
        {
            var token = _iuserService.login(users);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public IActionResult SignUp(Users users)
        {
            var rs = _iuserService.Insert(users);
            return Ok(rs);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("getbyuser")]
        public IActionResult GetByUser(string userName)
        {
            var rs = _iuserService.GetByUser(userName);
            return Ok(rs);
        }

        [Authorize]
        [HttpPost]
        [Route("changpassword")]
        public IActionResult ChangPassWord(UserChangPasswordModel userChangePass)
        {
            string userId = this.GetUserId();
            var rs = _iuserService.ChangPassword(userChangePass, userId);
            return Ok(rs);
        }
        
        [HttpPost]
        [Route("forgotpassword")]
        public IActionResult ForgotPassword()
        {
            var rs = _iuserService.ForgotPassword();
            return Ok(rs);
        }
        [HttpPost]
        [Route("sendmailkit")]
        public IActionResult SendMailKit(MailContent mailContent)
        {
            _iuserService.SendMail(mailContent);
            return Ok();
        }




    }
}
