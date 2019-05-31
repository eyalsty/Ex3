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
        // NEED TO BE FIX !
        public ActionResult Index()
        {
            ViewBag.Lon = 100;
            ViewBag.Lat = 50;
            return View();
        }

        #region DisplayOnce
        [HttpGet]
        public ActionResult DisplayOnce(string ip, int port)
        {
            InfoModel.Instance.EstablishConn(ip, port);
            this.BagCoordinate();

            InfoModel.Instance.Dissconnect();
            return View();
        }
        #endregion

        #region AutoDisplay
        [HttpGet]
        public ActionResult AutoDisplay(string ip, int port, int rate)
        {
            InfoModel.Instance.EstablishConn(ip, port);
            this.BagCoordinate();

            Session["rate"] = rate;
            
            return View();
        }
       
        [HttpPost]
        public string GetCoordinate()
        {
            Coordinate coordinate = InfoModel.Instance.GetCoordinate();
            return ToXml(coordinate);
        }
        #endregion

        #region SaveDisplay
        [HttpGet]
        public ActionResult SaveDisplay(string ip, int port, int rate, int duration, string fname)
        {
            InfoModel.Instance.EstablishConn(ip, port);
            this.BagCoordinate();

            // saves the rate and duration values for view().
            Session["rate"] = rate;
            Session["duration"] = duration;
            InfoModel.Instance.Fname = fname;

            return View();
        }

        [HttpPost]
        public string SaveFlightData()
        {
            // saving the flight data and return the coordinates.
            Coordinate coordinate = InfoModel.Instance.SaveFlightData();

            return this.ToXml(coordinate);
        }
        #endregion

        #region LoadDisplay
        [HttpGet]
        public ActionResult LoadDisplay(string fname, int rate)
        {
            Session["rate"] = rate;
            InfoModel.Instance.Fname = fname;
            
            return View();
        }

        [HttpPost]
        public string LoadFlightData()
        {
            if (!InfoModel.Instance.IsLoaded)
            {
                // load data from 'Fname', because it hasn't loaded yet.
                InfoModel.Instance.LoadFlightData();
            }

            // the next coordinate to present.
            var nextCoordinate = InfoModel.Instance.NextCoordinate;

            return (nextCoordinate != null) ? ToXml(nextCoordinate) : null;
        }
        #endregion

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

        private void BagCoordinate()
        {
            /* getts current coordinate from flightGear and Bag them for View(). */

            Coordinate c = InfoModel.Instance.GetCoordinate();
            ViewBag.Lon = c.Lon;
            ViewBag.Lat = c.Lat;
        }
    }
}