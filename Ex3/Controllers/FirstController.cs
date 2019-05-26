using Ex3.Utils;
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
            return View();
        }

        [HttpGet]
        public ActionResult DisplayOnce(string ip, int port)
        {
            Client client = new Client();
            client.Connect(ip, port);

            // saving the Lon & Lat values for the View.
            ViewBag.Lon = client.Get("/position/longitude-deg");
            ViewBag.Lat = client.Get("/position/latitude-deg");

            return View();
        }

        [HttpGet]
        public ActionResult AutoDisplay(string ip, int port)
        {
            return View();
        }

        [HttpGet]
        public ActionResult SaveDisplay(string ip, int port)
        {

            return View();
        }
    }
}