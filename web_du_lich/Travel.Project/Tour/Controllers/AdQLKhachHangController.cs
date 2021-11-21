using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Tour.Entites;

namespace Tour.Controllers
{
    public class AdQLKhachHangController : Controller
    {
        private string Base_URL = "https://localhost:44330/api/khachhang/";

        // GET: AdQLKhachHang
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("getall").Result;
            if (response.IsSuccessStatusCode)
            {
                var strResult = response.Content.ReadAsStringAsync().Result;
                var jsonData = JObject.Parse(strResult);
                var obj = jsonData["data"];
                var listobj = obj.ToObject<List<KhachHang>>();
                ViewBag.ListCount = listobj.Count;
                ViewBag.ListTour = listobj;
            }
            return View();
        }
        public ActionResult UserAccount()
        {
            return View();
        }
    }
}