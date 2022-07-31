using System.Net;
using System.Net.Sockets;
using System.Text;

const int PORT = 8088;
const string IP = "127.0.0.1";

Console.WriteLine("Client start...");
IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP), PORT);
Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{

clientSocket.Connect(iPEnd);

    byte[] data = new byte[1024];
    int recv = clientSocket.Receive(data);
    string s = Encoding.ASCII.GetString(data, 0, recv);
    Console.WriteLine("From Server : {0}", s);
    string input;

    while (true)
{

        Console.Write("Введите математическое выражение: ");
        string Mach = Console.ReadLine();
        if (Mach == "") break;

        data = new byte[1024];
        data = Encoding.ASCII.GetBytes(Mach);
        clientSocket.Send(data, data.Length, SocketFlags.None);

        recv = clientSocket.Receive(data);
        s = Encoding.UTF8.GetString(data, 0, recv);
        Console.WriteLine("{0} = {1}", Mach, s);
      
    }

    Console.ReadKey();
}
catch (SocketException ex)
{
    Console.WriteLine(ex.Message);
}



