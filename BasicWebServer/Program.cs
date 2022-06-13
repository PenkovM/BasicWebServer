using System.Net;
using System.Net.Sockets;
using System.Text;

var ipAddress = IPAddress.Parse("127.0.0.1");
var port = 8080;

//Using the TcpListener we can connect to the given addres and port.
var serverListener = new TcpListener(ipAddress, port);
serverListener.Start();




