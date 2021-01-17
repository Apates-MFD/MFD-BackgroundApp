using NetworkLibrary.NetworkPackage.Commands;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetworkLibrary
{
    public class Listener
    {
        public static readonly int LISTENER_PORT = 42069;
        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        private TcpListener listener;
        private TcpClient client;
        private IPAddress ip;
        private bool stop = false;

        public Listener(string addr)
        {
            ip = IPAddress.Parse(addr);          
        }

        private void nonBlockListen()
        {
            try
            {               
                client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                
                while (!stop)
                {
                    if(client.Available > 0)
                    {
                        byte[] buffer = new byte[client.Available];
                        stream.Read(buffer,0,buffer.Length);
                        if (CommandReceived != null)
                        {
                            object[] com = Package.Get(buffer);

                            CommandReceived.Invoke(this, new CommandReceivedEventArgs()
                            {
                                Command_type = (Command_Types)com[0],
                                Command = (int)com[1],
                                Args = (object[])com[2]
                            });
                        }
                    }
                }
               
            }
            catch (SocketException e)
            {
                throw e;
            }
            finally
            {
                listener.Stop();
            }
            
        }

        public void Start()
        {
            stop = false;
            listener = new TcpListener(ip, LISTENER_PORT);
            listener.Start();
            Thread t = new Thread(new ThreadStart(nonBlockListen));
            t.IsBackground = true;
            t.Start();
        }

        public void Stop()
        {
            stop = true;
        }
    }
}
