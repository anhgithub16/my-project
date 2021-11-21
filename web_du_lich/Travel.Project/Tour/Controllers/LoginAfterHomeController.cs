using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tour.Controllers
{
    public class LoginAfterHomeController : Controller
    {
        // GET: LoginAfterHome
        public ActionResult Index()
        {
            //if (TempData["name"] != null)
            //{
            //    TempData.Keep();
            //    ViewBag.Login = "Login";
            //    ViewData["login"] = "Login";
            //}
            ViewBag.Mess = "Display A Message";
            return View();
        }
    }
}