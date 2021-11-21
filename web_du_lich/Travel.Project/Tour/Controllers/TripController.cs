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

    public class TripController : Controller
    {
        private string Base_URL = "https://localhost:44330/api/trip/";

        // GET: Trip
        public ActionResult Index(string keyword)
        {
            List<Trip> list = new List<Trip>();
            if (keyword == null)
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
                    var listobj = obj.ToObject<List<Trip>>();
                    list = listobj;
                }
            }
            else
            {
                // xu ly search
                HttpClient client1 = new HttpClient();
                client1.BaseAddress = new Uri(Base_URL);
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response1 = client1.GetAsync("search?keyword="+keyword).Result;
                if (response1.IsSuccessStatusCode)
                {
                    var strResult = response1.Content.ReadAsStringAsync().Result;
                    var jsonData = JObject.Parse(strResult);
                    var obj = jsonData["data"];
                    var listobj = obj.ToObject<List<Trip>>();
                    list = listobj;
                    ViewBag.TieuDe = "Kết quả tìm kiếm với từ khóa: " + keyword;
                }
            }
            //Response to view
            ViewBag.ListCount = list.Count;
            ViewBag.ListTrip = list;
            return View();
        }
        public ActionResult DisplayTrip(string id)
        {
            // Get information of trip by id
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("getbyid?id="+id).Result;
            if (response.IsSuccessStatusCode)
            {
                var strResult = response.Content.ReadAsStringAsync().Result;
                var jsonData = JObject.Parse(strResult);
                var obj = jsonData["data"];
                var listobj = obj.ToObject<Trip>();
                ViewBag.ListTrip = listobj;
            }
            return View();
        }
    }
}