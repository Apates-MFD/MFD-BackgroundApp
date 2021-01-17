using NetworkLibrary.NetworkPackage.Commands;
using System;
using System.Threading;

namespace NetworkLibrary
{
    class Test
    {
        static void Main(string[] args)
        {
            byte[] p = Package.Create(Command_Types.BUTTONS, Commands_Button.SET_TEXT);
            object[] command = Package.Get(p);
           /* Listener listener = new Listener("127.0.0.1");
            listener.CommandReceived += Listener_CommandReceived;
            listener.Start();*/
            Writer writer = new Writer("127.0.0.1");
            Console.WriteLine("Type\t\tCommand\t\tArguments");
            while (true)
            {
                writer.write(p);
                Thread.Sleep(1000);
            }
        }

        private static void Listener_CommandReceived(object sender, CommandReceivedEventArgs e)
        {
            Command_Types type = e.Command_type;
            string command;
            switch (type)
            {
                case Command_Types.BUTTONS:
                    command = ((Commands_Button)e.Command).ToString();
                    break;
                default: return;
            }
            Console.Write("{0}\t\t{1}\t\t", type.ToString(), command);
            if (e.Args != null)
            {
                foreach (object obj in e.Args)
                {
                    Console.Write(" {0} ", obj.ToString());
                }
            }
            Console.WriteLine("");
        }
    }
}
