using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Tour.Entites;

namespace Tour.Controllers
{
    public class DetailsTourController : Controller
    {
        private string Base_URL = "https://localhost:44330/api/tourdetail/";
        // GET: DetailsTour
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("getbycityid?cityid="+id).Result;
            if (response.IsSuccessStatusCode)
            {
                var strResult = response.Content.ReadAsStringAsync().Result;
                var jsonData = JObject.Parse(strResult);
                var obj = jsonData["data"];
                var listobj = obj.ToObject<List<TourDetail>>();
                ViewBag.ListCount = listobj.Count;
                ViewBag.ListTour = listobj;
            }
            return View();
        }
        public ActionResult DisplayDetail()
        {
           
            return View();

        }
    }
}