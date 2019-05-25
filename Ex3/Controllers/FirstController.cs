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