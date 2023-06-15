using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dictionary = new()
            {
                ["Ukraine"] = "Kyiv",
                ["Poland"] = "Warsaw",
                ["Germany"] = "Berlin",
                ["Belgium"] = "Brussels",
                ["Brazil"] = "Brasília",
                ["Bulgaria"] = "Sofia",
                ["Cambodia"] = "Phnom Penh",
                ["Canada"] = "Ottawa",
                ["China"] = "Beijing",
                ["Colombia"] = "Bogotá",
                ["Cuba"] = "Havana",
                ["Dominica"] = "Roseau",
                ["Egypt"] = "Cairo"
            };

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.1.106"), 3344);

            UdpClient server = new UdpClient(endPoint);

            while (true)
            {
                Console.WriteLine("...Waiting for the request...");

                IPEndPoint clientEndPoint = null;

                byte[] request = server.Receive(ref clientEndPoint);

                string message = Encoding.UTF8.GetString(request);
                Console.WriteLine($"Received message: {message} : {DateTime.Now.ToShortTimeString()} from {clientEndPoint}");
                string responses = " ";
                if (dictionary.ContainsKey(message))
                    responses = dictionary[message];
                else
                    responses = "I can't find this country";
                byte[] response = Encoding.UTF8.GetBytes(responses);
                server.Send(response, response.Length, clientEndPoint);
            }
        }
    }
}
