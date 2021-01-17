using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetworkLibrary
{
    class Test
    {
        static void Main(string[] args)
        {
            byte[] p = Package.Create(COMMAND_TYPES.BUTTONS, COMMAND_BUTTONS.SET_TEXT, 14, "HELLO");
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
            COMMAND_TYPES type = e.Command_type;
            string command;
            switch (type)
            {
                case COMMAND_TYPES.BUTTONS:
                    command = ((COMMAND_BUTTONS)e.Command).ToString();
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
