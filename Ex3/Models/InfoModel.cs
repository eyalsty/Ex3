using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class InfoModel
    {
        #region Singleton
        private static InfoModel s_instace = null;
        public static InfoModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new InfoModel();
                }
                return s_instace;
            }
        }
        #endregion

        public InfoModel()
        {
            client = new Client();
        }

        public Client client { get; private set; }

        public string Fname { get; set; }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";

        public Coordinate SaveFlightData()
        {
            // getting the flight data values.
            double lon = client.Get("/position/longitude-deg");
            double lat = client.Get("/position/latitude-deg");
            double throttle = client.Get("/controls/engines/current-engine/throttle");
            double rudder = client.Get("/controls/flight/rudder");

            // getting the path of the file.
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, this.Fname));

            using (StreamWriter file = new StreamWriter(path, true))
            {
                // saving the flight data values.
                file.WriteLine(lon.ToString());
                file.WriteLine(lat.ToString());
                file.WriteLine(throttle.ToString());
                file.WriteLine(rudder.ToString());
            }

            // return the lon and lat value to the controller.
            return new Coordinate(lon, lat);
 
        }

        public Coordinate GetCoordinate()
        {
            double lon = client.Get("/position/longitude-deg");
            double lat = client.Get("/position/latitude-deg");
            return new Coordinate(lon, lat);
        }
       
    }
}