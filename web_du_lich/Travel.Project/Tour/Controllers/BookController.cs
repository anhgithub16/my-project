using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Tour.Entites;
using Tour.Models;

namespace Tour.Controllers
{
    public class BookController : Controller
    {
        private string Base_URL = "https://localhost:44330/api/tourdetail/";
        private string Base_URL_KhachHang = "https://localhost:44330/api/khachhang/";
        private string Base_URL_BookTour = "https://localhost:44330/api/booktour/";
        private string Base_URL_Trip = "https://localhost:44330/api/trip/";
        private string Base_URL_SendMail = "https://localhost:44330/api/sendmail/";


        // book hotel
        [HttpGet]
        public ActionResult Index(string id)
        {
            // hotel 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("getbyid?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var strResult = response.Content.ReadAsStringAsync().Result;
                var jsonData = JObject.Parse(strResult);
                var obj = jsonData["data"];
                var tour = obj.ToObject<TourDetail>();
                ViewBag.Tour = tour;
                ViewBag.TenTour = tour.TenTour;
                TempData["tour"] = tour.Id;
                TempData["tourname"] = tour.TenTour;
                TempData["GiaTour"] = tour.GiaTour;
                TempData["saleTour"] = tour.Sale;
            }

            

            return View();
        }
        [HttpPost]
        public ActionResult Index(TourKhachHang tour)
        {
            long giaTour = 0;
            if (TempData.ContainsKey("tour"))
            {
                tour.bookTour.TourDetailId = Guid.Parse(TempData["tour"].ToString());

            }
            if (TempData.ContainsKey("tourname"))
            {
                ViewBag.TenTour = TempData["tourname"].ToString();
            }
            if (TempData.ContainsKey("GiaTour"))
            {
                long a = long.Parse(TempData["GiaTour"].ToString());
                long sale = long.Parse(TempData["saleTour"].ToString());
                giaTour = a - a * sale/100;
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL_KhachHang);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync("insert", tour.khachHang).Result;
            if (response.IsSuccessStatusCode)
            {
                HttpClient client1 = new HttpClient();
                client1.BaseAddress = new Uri(Base_URL_KhachHang);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response1 = client.GetAsync("searchkh?keyword=" + tour.khachHang.FullName).Result;
                if (response1.IsSuccessStatusCode)
                {
                    var strResult = response1.Content.ReadAsStringAsync().Result;
                    var jsonData = JObject.Parse(strResult);
                    var obj = jsonData["data"];
                    var tourDetail = obj.ToObject<TourDetail>();
                    tour.bookTour.KhachHangId = tourDetail.Id;
                    tour.bookTour.TongTien = giaTour * tour.bookTour.NguoiLon;

                    //
                    HttpClient client2 = new HttpClient();
                    client2.BaseAddress = new Uri(Base_URL_BookTour);
                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response2 = client2.PostAsJsonAsync("insert", tour.bookTour).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        //send mail
                        TempData["To"] = tour.khachHang.Email;
                        string body = "<h1> Xin chào " + tour.khachHang.FullName + "</h1></br>" +
                            "Qúy khách đã đăng kí tour " + TempData["tourname"] + " " +
                            "với " + tour.bookTour.NguoiLon + " người lớn và " + tour.bookTour.TreEm + " trẻ em </br>" +
                            "Giá Tour là : " + string.Format("{0:#,##0}", giaTour) + " đ/người </br>" +
                            "Tổng số tiền quý khách phải thanh toán là: " + string.Format("{0:#,##0}", (giaTour * tour.bookTour.NguoiLon)) + " đ";
                        TempData["Body"] = body;
                        return RedirectToAction("SetBook");
                    }
                }

                //
               
            }
            return View();
        }

        //Book Trip
        [HttpGet]
        public ActionResult BookTrip(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL_Trip);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("getbyid?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var strResult = response.Content.ReadAsStringAsync().Result;
                var jsonData = JObject.Parse(strResult);
                var obj = jsonData["data"];
                var trip = obj.ToObject<Trip>();
                ViewBag.Trip = trip;
                ViewBag.TenTrip = trip.Name;
                TempData["tripid"] = trip.Id;
                TempData["tripname"] = trip.Name;
                TempData["GiaTrip"] = trip.GiaTrip;
                TempData["sale"] = trip.Sale;
                var a = trip.GiaTrip;
                var b = trip.Sale;
                var c = a - a * (b / 100);
                TempData["GiaSauSale"] = c;
            }
            return View();
        }
        [HttpPost]
        public ActionResult BookTrip(TourKhachHang tour)
        {
            long gia = 0;
            if (TempData.ContainsKey("tripid"))
            {
                tour.bookTour.TripId = Guid.Parse(TempData["tripid"].ToString());

            }
            if (TempData.ContainsKey("tripname"))
            {
                ViewBag.TenTrip = TempData["tripname"].ToString();
            }
            if (TempData.ContainsKey("GiaTrip"))
            {
                var a  = long.Parse(TempData["GiaTrip"].ToString());
                long sale = long.Parse(TempData["sale"].ToString());
                gia = a - a * sale/100;
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL_KhachHang);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync("insert", tour.khachHang).Result;
            if (response.IsSuccessStatusCode)
            {
                HttpClient client1 = new HttpClient();
                client1.BaseAddress = new Uri(Base_URL_KhachHang);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response1 = client.GetAsync("searchkh?keyword=" + tour.khachHang.FullName).Result;
                if (response1.IsSuccessStatusCode)
                {
                    var strResult = response1.Content.ReadAsStringAsync().Result;
                    var jsonData = JObject.Parse(strResult);
                    var obj = jsonData["data"];
                    var tourDetail = obj.ToObject<TourDetail>();
                    tour.bookTour.KhachHangId = tourDetail.Id;
                    tour.bookTour.TongTien = gia * tour.bookTour.NguoiLon;
                    //
                    HttpClient client2 = new HttpClient();
                    client2.BaseAddress = new Uri(Base_URL_BookTour);
                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response2 = client2.PostAsJsonAsync("insert", tour.bookTour).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        //send mail
                        TempData["To"] = tour.khachHang.Email;
                        string body = "<h1> Xin chào " + tour.khachHang.FullName + "</h1></br>"+ 
                            "Qúy khách đã đăng kí tour " + TempData["tripname"] +" " +
                            "với "+tour.bookTour.NguoiLon+" người lớn và "+tour.bookTour.TreEm+" trẻ em </br>"+
                            "Giá Tour là : "+string.Format("{0:#,##0}",gia)+" đ/người </br>"+
                            "Tổng số tiền quý khách phải thanh toán là: "+string.Format("{0:#,##0}", (gia*tour.bookTour.NguoiLon)) +" đ";
                        TempData["Body"] = body;
                        return RedirectToAction("SetBook");
                    }
                }

                //

            }
            return View();

        }
        
        // set book
        public ActionResult SetBook(MailContent mailContent)
        {
            mailContent.To = TempData["To"].ToString();
            mailContent.Subject = "Chào mứng bạn đến với Ivivu.com";
            mailContent.Body = TempData["Body"].ToString(); 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Base_URL_SendMail);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync("sendmail", mailContent).Result;
            if (response.IsSuccessStatusCode)
            {
                return View();
            }
            return View();
        }
    }
}