﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;

        public HttpServer(string _ipAddress, int _port)
        {
            ipAddress = IPAddress.Parse(_ipAddress);
            port = _port;

            //Using the TcpListener we can connect to the given addres and port.
            serverListener = new TcpListener(ipAddress, port);
        }

        public void Start()
        {
            serverListener.Start();

            Console.WriteLine($"Server started at port {port}...");
            Console.WriteLine("Listening for requests....");

            while (true)
            {
                var connection = serverListener.AcceptTcpClient();

                //Through the Stream we receive and send data to the browser as a byte array.
                var networkStream = connection.GetStream();
                WriteResponse(networkStream, "Hello from the server!");

                connection.Close();
            }
        }
        private void WriteResponse(NetworkStream networkStream, string message)
        {
            var contentLength = Encoding.UTF8.GetByteCount(message);

            var response = $@"HTTP/1.1 200 OK
Content-Type: text/plain; charset=UTF-8
Content-Length: {contentLength}

{message}";

            var responseBytes = Encoding.UTF8.GetBytes(response);
            networkStream.Write(responseBytes, 0, responseBytes.Length);
        }
    }
}
