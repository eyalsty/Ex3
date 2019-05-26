using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace Ex3.Utils
{
    public class Client
    {
        private TcpClient tcpClient;

        public Client()
        {
            tcpClient = new TcpClient();
        }

        public void Connect(string ip, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            tcpClient.Connect(endPoint);
        }

        public double Get(string requestPath)
        {
            int size = 2048;
            byte[] response = new byte[size];

            if (tcpClient.Connected)
            {          
                NetworkStream stream = tcpClient.GetStream();

                byte[] encoded = Encoding.ASCII.GetBytes("get " + requestPath + "\r\n");
                stream.Write(encoded, 0, encoded.Length);
                stream.Flush();

                stream.Read(response, 0, size);
            }

            // getting the value from the response format of the server .
            string num = Encoding.ASCII.GetString(response, 0, size).Split(' ')[2];

            // Get rid of quotation marks and converting the number to Double .
            return Convert.ToDouble(num.Substring(1, num.Length - 2));
        }

        public void Dissconnect()
        {
            tcpClient.Close();
        }
    }
}