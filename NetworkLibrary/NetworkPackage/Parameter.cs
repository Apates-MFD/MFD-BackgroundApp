using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NetworkLibrary
{
    public static class Parameter
    {
        public static readonly byte MAGIC = 0b01010101;
        private static readonly byte PARAMETER_SIZE_BYTE_COUNT = 4;
        private static readonly byte PARAMETER_TYPE_SIZE_BYTE_COUNT = 4;

        /// <summary>
        /// Creates Parameter Array for given paramter
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static byte[] Create(object parameter)
        {
            //Null Check
            if (parameter == null) return null;

            //Vars
            byte[] para_type_size;
            byte[] para_type;
            byte[] para_size;
            byte[] para;
            //Save Type
            para_type = Encoding.ASCII.GetBytes(parameter.GetType().ToString().Replace("System.", ""));

            //Save Parameter Size
            uint pt = (uint)para_type.Length;
            para_type_size = BitConverter.GetBytes(pt);

            //Save Parameter        
            para = Encoding.ASCII.GetBytes(parameter.ToString());

            //Save Parameter Size
            uint ps = (uint)para.Length;
            para_size = BitConverter.GetBytes(ps);

            //Create Array
            byte[] pa = new byte[1 + PARAMETER_SIZE_BYTE_COUNT + PARAMETER_TYPE_SIZE_BYTE_COUNT + pt + ps];
            pa[0] = MAGIC;
            Array.Copy(para_type_size, 0, pa,1, para_type_size.Length);
            Array.Copy(para_size, 0, pa, 1 + para_type_size.Length, para_size.Length);
            Array.Copy(para_type, 0, pa, para_size.Length + 1 + para_type_size.Length, para_type.Length);
            Array.Copy(para, 0, pa, para_size.Length + 1 + para_type_size.Length + para_type.Length, para.Length);
            return pa;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="para_type_size"></param>
        /// <param name="para_size"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Get(uint para_type_size, uint para_size, byte[] data)
        {
            byte[] type = new byte[para_type_size];
            byte[] para = new byte[para_size];
            Array.Copy(data, 0, type, 0, para_type_size);
            Array.Copy(data, para_type_size, para, 0, para_size);

            string typeS = Encoding.ASCII.GetString(type);
            string paraS = Encoding.ASCII.GetString(para);
            object obj;
            switch (typeS)
            {
                case "Int32":
                    obj = int.Parse(paraS);
                    break;
                case "String":
                    obj = paraS;
                    break;

                default: throw new Exception("Unsuported Data Type");
            }
            return obj;    
        }
    }
}
