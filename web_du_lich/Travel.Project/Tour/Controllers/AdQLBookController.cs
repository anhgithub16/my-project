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
    public class AdQLBookController : Controller
    {
        private string Base_URL = "https://localhost:44330/api/booktour/";

        // GET: AdQLBook
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DisplayBookHotel()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("getallhotel").Result;
            if (response.IsSuccessStatusCode)
            {
                var strResult = response.Content.ReadAsStringAsync().Result;
                var jsonData = JObject.Parse(strResult);
                var obj = jsonData["data"];
                var listobj = obj.ToObject<List<BookTour>>();
                ViewBag.ListCount = listobj.Count;
                ViewBag.ListTour = listobj;
            }
            return View();
        }
        public ActionResult DisplayBookTrip()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("getalltrip").Result;
            if (response.IsSuccessStatusCode)
            {
                var strResult = response.Content.ReadAsStringAsync().Result;
                var jsonData = JObject.Parse(strResult);
                var obj = jsonData["data"];
                var listobj = obj.ToObject<List<BookTour>>();
                ViewBag.ListCount = listobj.Count;
                ViewBag.ListTour = listobj;
            }
            return View();
        }
    }
}