using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class InfoModel
    {
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

        public Client client { get; private set; }

        public InfoModel()
        {
            client = new Client();
        }
       
    }
}