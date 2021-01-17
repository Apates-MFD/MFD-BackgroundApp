using NetworkLibrary.NetworkPackage.Commands;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetworkLibrary
{
    public static class Package
    {
        public static readonly byte MAGIC = 0b10101010;
        private static readonly byte HEADER_SIZE = 8;
        public static byte[] Create(Command_Types _command_type, Commands_Button _command, params object[] args)
        {
            int command_type = (int)_command_type;
            int command = (int)_command;

            //Command Type && Command
            byte com_t_c = (byte)((command_type << 6) | (command & 0b00111111));

            //Parameter Count
            byte para_c = (byte)(args == null ? 0 : args.Length);

            //Parameters
            List<byte[]> list = new List<byte[]>();
            int parameter_sizes = 0;
            for(int i = 0; i < para_c; i++)
            {
#pragma warning disable CS8602
                byte[] conv = Parameter.Create(args[i]);
#pragma warning restore CS8602
                if (conv == null) throw new Exception("Converting Parameter to Array failed");
                parameter_sizes += conv.Length;
                list.Add(conv);
            }
            byte[] parameters = new byte[parameter_sizes];
            int index = 0;
            foreach(byte[] arr in list)
            {
                foreach(byte b in arr)
                {
                    parameters[index++] = b;
                }
            }

            //Prepare Package
            uint _package_size = (uint)(8 + parameter_sizes);
            byte[] package_size = BitConverter.GetBytes(_package_size);
            byte[] package = new byte[_package_size];

            //Build Package
            package[0] = MAGIC;
            Array.Copy(package_size, 0, package, 1, package_size.Length);
            package[5] = HEADER_SIZE;
            package[6] = com_t_c;
            package[7] = para_c;
            Array.Copy(parameters, 0, package, 8, parameters.Length);

            //Return
            return package;

        }

        public static object[] Get(byte[] package)
        {
            Command_Types command_type;
            Commands_Button command;
            object[] args = null;

            using(MemoryStream stream = new MemoryStream(package))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                //Magic
                if (reader.ReadByte() != MAGIC)
                {
                    throw new Exception("Magic Mismatches");
                }

                //Package Size
                if(reader.ReadUInt32() != package.Length)
                {
                    throw new Exception("Package size mismatches");
                }

                //HeaderSize
                if(reader.ReadByte() != HEADER_SIZE)
                {
                    throw new Exception("Header size mismatches");
                }

                //Command Type & Command
                byte com_t_c = reader.ReadByte();
                int ct = com_t_c & 0b11000000;
                ct = ct >> 6;

                int cm = com_t_c & 0b00111111;

                command_type = (Command_Types)ct;
                command = (Commands_Button)cm;

                //Parameter Count
                byte parameter_count = reader.ReadByte();

                if(parameter_count == 0)
                {
                    return new object[] { command_type, command, args };
                }

                args = new object[parameter_count];
                //Parameters
                for(int i = 0; i < parameter_count; i++)
                {
                    if (reader.ReadByte() != Parameter.MAGIC) throw new Exception("Parameter magic mismatches");
                    uint para_type_size = reader.ReadUInt32();
                    uint para_size = reader.ReadUInt32();
                    byte[] param_arr = reader.ReadBytes((int)(para_type_size+para_size));
                    args[i] = Parameter.Get(para_type_size, para_size, param_arr);
                }

                return new object[] { command_type, command, args };
            }
        }
    }
}
