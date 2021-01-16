using System.Net.Sockets;

namespace NetworkLibrary
{
    public class Writer
    {
        private TcpClient client;
        public Writer(string ipaddress)
        {
            client = new TcpClient(ipaddress,Listener.LISTENER_PORT);
        }

        public void write(byte[] data)
        {
            client.GetStream().Write(data, 0, data.Length);
        }
    }
}
