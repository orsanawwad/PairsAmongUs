using Newtonsoft.Json;
using SocketIOClient.WebSocketClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace PairsAmongUsCMD
{
    class Program
    {

        static void Main(string[] args)
        {
            new Capture().Init();
            Console.ReadLine();
        }
    }
}