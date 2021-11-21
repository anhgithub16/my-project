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
    public class HotelController : Controller
    {
        private string Base_URL = "https://localhost:44330/api/tourdetail/";
        private string Base_URL_Combo = "https://localhost:44330/api/combo/";

        public ActionResult Index(string id,string keyword)
        {
            List<TourDetail> list = new List<TourDetail>();

            if (id == null)
            {
                HttpClient client1 = new HttpClient();
                client1.BaseAddress = new Uri(Base_URL);
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response1 = client1.GetAsync("search?keyword=" + keyword).Result;
                if (response1.IsSuccessStatusCode)
                {
                    var strResult1 = response1.Content.ReadAsStringAsync().Result;
                    var jsonData = JObject.Parse(strResult1);
                    var obj = jsonData["data"];
                    var listobj = obj.ToObject<List<TourDetail>>();
                    list = listobj;
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("getbycityid?cityid=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var strResult = response.Content.ReadAsStringAsync().Result;
                    var jsonData = JObject.Parse(strResult);
                    var obj = jsonData["data"];
                    var listobj = obj.ToObject<List<TourDetail>>();
                    list = listobj;
                }
            }
            ViewBag.ListCount = list.Count;
            ViewBag.ListTour = list;
            return View();
        }
        public ActionResult DisplayDetail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //tourdetail
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("getbyid?id=" + id).Result;
            string tourDetailId = null;
            if (response.IsSuccessStatusCode)
            {
                var strResult = response.Content.ReadAsStringAsync().Result;
                var jsonData = JObject.Parse(strResult);
                var obj = jsonData["data"];
                var listobj = obj.ToObject<TourDetail>();
                tourDetailId = listobj.Id.ToString();
                ViewBag.ListTour = listobj;
                
            }
            //combo
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri(Base_URL_Combo);
            client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response2 = client1.GetAsync("getbytourdetailid?tourDetailId=" + tourDetailId).Result;
            if (response2.IsSuccessStatusCode)
            {
                var strResult1 = response2.Content.ReadAsStringAsync().Result;
                var jsonData1 = JObject.Parse(strResult1);
                var obj1 = jsonData1["data"];
                var objRes = obj1.ToObject<Combo>();
                ViewBag.ResCombo = objRes;
            }
            return View();

        }
    }
}