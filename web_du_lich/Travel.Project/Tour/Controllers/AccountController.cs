using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Tour.Entites;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Tour.Controllers
{
    public class AccountController : Controller
    {
        private string Base_URL = "https://localhost:44320/api/user/";
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (TempData.ContainsKey("signup"))
            {
                ViewBag.Mes = TempData["signup"].ToString();
            }
            ViewBag.Message = "Login";
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Users users)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage res = client.PostAsJsonAsync("login", users).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["name"] = "a name";
                return RedirectToAction("Index","Admin");
            }
            else
            {
                ModelState.AddModelError("falied", "Đăng nhập thất bại");
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(Users users)
        {
            if (ModelState.IsValid)
            {
                HttpClient client1 = new HttpClient();
                client1.BaseAddress = new Uri(Base_URL);
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client1.GetAsync("getbyuser?username=" + users.UserName).Result;
                if (res.IsSuccessStatusCode)
                {
                    var strRes = res.Content.ReadAsStringAsync().Result;
                    var jsonRes = JObject.Parse(strRes);
                    var data = jsonRes["data"];
                    var listUser = data.ToObject<List<Users>>();
                    if(listUser.Count == 0)
                    {
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(Base_URL);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage response = client.PostAsJsonAsync("signup", users).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            TempData["signup"] = "Sign Up Success";
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                            ModelState.AddModelError("signup", "Đăng ký thất bại");
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("userExist", "UserName already exist");
                        return View();
                    }
                }
            }
            return View();

        }


    }
}
