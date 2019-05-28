using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ex3.Controllers
{
    public class FirstController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            ViewBag.Lon = 100;
            ViewBag.Lat = 50;
            return View();
        }
        //ADD CHECK
        [HttpGet]
        public ActionResult DisplayOnce(string ip, int port)
        {
            Client client = new Client();
            client.Connect(ip, port);

            // saving the Lon & Lat values for the View.
            ViewBag.Lon = client.Get("/position/longitude-deg");
            ViewBag.Lat = client.Get("/position/latitude-deg");

            client.Dissconnect();
            return View();
        }

        [HttpGet]
        public ActionResult AutoDisplay(string ip, int port, int rate)
        {
            Client client = InfoModel.Instance.client;
            client.Connect(ip, port);

            ViewBag.Lon = client.Get("/position/longitude-deg");
            ViewBag.Lat = client.Get("/position/latitude-deg");

            Session["rate"] = rate;


            return View();
        }

        [HttpPost]
        public double GetLat()
        {
            Client client = InfoModel.Instance.client;
            return client.Get("/position/latitude-deg");
        }

        [HttpPost]
        public double GetLon()
        {
            Client client = InfoModel.Instance.client;
            return client.Get("/position/longitude-deg");
        }

        [HttpGet]
        public ActionResult SaveDisplay(string ip, int port)
        {

            return View();
        }
    }
}