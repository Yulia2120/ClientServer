using System.Net;
using System.Net.Sockets;
using System.Text;
//server
const int PORT = 8088;
const string IP = "127.0.0.1";

Console.WriteLine("Server start...");
IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP), PORT);
Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try {
    serverSocket.Bind(iPEnd);
    serverSocket.Listen(10);
    Console.WriteLine($"Server listen on {IP}:{PORT}");
    Socket clientSocket = serverSocket.Accept();

    Console.WriteLine("Accept {0}", clientSocket.RemoteEndPoint.ToString());

    string s = "Welcome";
    byte[] data = new byte[1024]; //буфер отправки
    data = Encoding.ASCII.GetBytes(s);
    clientSocket.Send(data, data.Length, SocketFlags.None);

    byte[] total = new byte[1024];

    string array;
    while (true)
    {
        int recv = clientSocket.Receive(data);
        array = Encoding.ASCII.GetString(data, 0, recv);
        Console.WriteLine("Client: {0}", array); // принял от клиента строку
        
        total = Encoding.ASCII.GetBytes(Operation(array).ToString()); // посчитал и отправил клиенту результат
        clientSocket.Send(total, total.Length, SocketFlags.None);


    }
  
}
catch (SocketException ex)
{
    Console.WriteLine(ex.Message);
}


static double Operation(string a)
{
    int found0 = a.LastIndexOf("+");
    if (found0 >= 0)
        return Operation(a.Substring(0, found0)) + Operation(a.Substring(found0 + 1));
    int found1 = a.LastIndexOf("-");
    if (found1 >= 0)
        return Operation(a.Substring(0, found1)) - Operation(a.Substring(found1 + 1));
    int found2 = a.LastIndexOf("*");
    if (found2 >= 0)
        return Operation(a.Substring(0, found2)) * Operation(a.Substring(found2 + 1));
    int found3 = a.LastIndexOf("/");
    if (found3 >= 0)
        return Operation(a.Substring(0, found3)) / Operation(a.Substring(found3 + 1));
    int found4 = a.LastIndexOf("^");
    if (found4 >= 0)
        return Math.Pow(Operation(a.Substring(0, found4)), Operation(a.Substring(found4 + 1)));
    return Convert.ToInt32(a);
}





Console.ReadLine(); 