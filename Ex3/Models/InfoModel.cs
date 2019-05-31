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

        private const string SCENARIO_FILE = "~/App_Data/{0}.txt";
        private const string lonPath = "/position/longitude-deg";
        private const string latPath = "/position/latitude-deg";
        private const string throttlePath = "/controls/engines/current-engine/throttle";
        private const string rudderPath = "/controls/flight/rudder";

        public InfoModel()
        {
            Client = new Client();
        }

        #region Properties
        public Client Client { get; private set; }

        public bool IsLoaded { get;  private set; } // indication if the flightData file was loaded elready.

        public Coordinate NextCoordinate
        {
            get
            {
                /* if the file loaded and there's still coordinate 
                 * to present, then return it. */

                return (IsLoaded && flightData.Count != 0) ? flightData.Dequeue() : null;
            }
        }

        private string fname = null;
        public string Fname
        {
            get { return fname; }
            set
            {
                fname = value;
                
                // the file name modified, so it hasn't loaded yet.
                this.IsLoaded = false;
            }
        }
        #endregion

        #region Save
        public Coordinate SaveFlightData()
        {
            // getting the flight data values.
            double[] flightData = { Client.Get(lonPath), Client.Get(latPath),
                Client.Get(throttlePath), Client.Get(rudderPath) };

            // gets the path of the file.
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, this.Fname));

            using (StreamWriter file = new StreamWriter(path, true))
            {
                // saves the flight data values.
                foreach(double element in flightData)
                {
                    file.WriteLine(element.ToString());
                }
            }

            // return the lon and lat value to the controller.
            return new Coordinate(flightData[0], flightData[1]);
        }
        #endregion
       
        #region Load
        private Queue<Coordinate> flightData;
        public void LoadFlightData()
        {
            flightData = new Queue<Coordinate>();
            string path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, this.Fname));

            if (File.Exists(path))
            {
                string[] elements = File.ReadAllLines(path);

                for (int i = 0; i < elements.Length; i += 4)
                {
                    double lon = Convert.ToDouble(elements[i]);
                    double lat = Convert.ToDouble(elements[i + 1]);

                    // save the next and correct coordinate for later present.
                    flightData.Enqueue(new Coordinate(lon, lat));
                }
            }

            // to indicate that the file is loaded.
            this.IsLoaded = true;
        }
        #endregion

        public Coordinate GetCoordinate()
        {
            /* returning current plain location streight from the flightGear. */

            double lon = Client.Get(lonPath);
            double lat = Client.Get(latPath);
            return new Coordinate(lon, lat);
        }

        public void EstablishConn(string ip, int port)
        {
            /* establishing connection with the flightGear. if 'Client' already
             * connected, than dissconnecting it, and starting a new session. */

            if(this.Client.IsConnected())
            {
                this.Client.Dissconnect();
            }

            this.Client.Connect(ip, port);
            
        }

        public void Dissconnect()
        {
            this.Client.Dissconnect();
        }
    }
}