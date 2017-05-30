﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace GobangServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpHelperServer ths = new TcpHelperServer();
            int i = 0;
            while (true) 
            {
                Console.WriteLine("server" + ths.Reader());
                ths.Writer(i + "  200");
                i++;
            }
        }
    }
    public class TcpHelperServer
    {
        private static string ServerAddress = "127.0.0.1";
        private static int ServerPort = 9961;
        private static IPAddress ServerIPAddress = IPAddress.Parse(ServerAddress);
        private static IPEndPoint ServerIPEndPoint = new IPEndPoint(ServerIPAddress, ServerPort);
        private TcpListener TcpListener = null;
        private TcpClient TcpClient = null;
        private NetworkStream TcpStream = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;

        public TcpHelperServer()
        {
            TcpListener = new TcpListener(ServerIPEndPoint);
            TcpListener.Start();
            TcpClient = TcpListener.AcceptTcpClient();
            TcpStream = TcpClient.GetStream();
            sr = new StreamReader(TcpStream);
            sw = new StreamWriter(TcpStream);
        }
        public void Writer(string message)
        {
            sw.WriteLine(message);
            sw.Flush();
        }
        public string Reader()
        {
            return sr.ReadLine();
        }
    }
}
