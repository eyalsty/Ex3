using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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
        
        [HttpGet]
        public ActionResult DisplayOnce(string ip, int port)
        {
            Client client = InfoModel.Instance.client;
            this.EstablishConnection(client, ip, port);

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
            this.EstablishConnection(client, ip, port);

            ViewBag.Lon = client.Get("/position/longitude-deg");
            ViewBag.Lat = client.Get("/position/latitude-deg");

            Session["rate"] = rate;
            
            // client.Dissconect();     // maybe needed.
            return View();
        }

        [HttpGet]
        public ActionResult SaveDisplay(string ip, int port, int rate, int duration, string fname)
        {
            // getting the client and creating connection with the flightGear.
            Client client = InfoModel.Instance.client;
            this.EstablishConnection(client, ip, port);

            // saving the Lot and Lan values for the View().
            ViewBag.Lon = client.Get("/position/longitude-deg");
            ViewBag.Lat = client.Get("/position/latitude-deg");

            // saving the rate and duration values for the view().
            Session["rate"] = rate;
            Session["duration"] = duration;
            InfoModel.Instance.Fname = fname;

            // client.Dissconnect();
            return View();
        }

        [HttpPost]
        public string GetCoordinate()
        {
            Client client = InfoModel.Instance.client;

            Coordinate coordinate = new Coordinate();
            coordinate.Lon = client.Get("/position/longitude-deg");
            coordinate.Lat = client.Get("/position/latitude-deg");

            return ToXml(coordinate);
        }

        [HttpPost]
        public string SaveFlightData()
        {
            // saving the flight data and return the coordinates.
            Coordinate coordinate = InfoModel.Instance.SaveFlightData();

            return this.ToXml(coordinate);
        }

        private string ToXml(Coordinate coordinate)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Coordinate");

            coordinate.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();

            return sb.ToString();
        }

        private void EstablishConnection(Client client, string ip, int port)
        {
            // help method for avoiding duplicate code.
            if (client.IsConnected())
            {
                client.Dissconnect();
            }
            client.Connect(ip, port);
        }
    }
}