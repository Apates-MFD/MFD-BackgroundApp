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
            byte[] para_size;
            byte[] para;

            //Save Parameter        
            using (var stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, parameter);
                para = stream.ToArray();
            }

            //Save Parameter Size
            uint ps = (uint)para.Length;
            para_size = BitConverter.GetBytes(ps);
            if (para_size.Length != PARAMETER_SIZE_BYTE_COUNT) throw new Exception("Parameter Size Byte count is wrong");

            //Create Array
            byte[] pa = new byte[1 + PARAMETER_SIZE_BYTE_COUNT + ps];
            pa[0] = MAGIC;
            Array.Copy(para_size, 0, pa,1, para_size.Length);
            Array.Copy(para, 0, pa, para_size.Length + 1, para.Length);

            return pa;
        }

        /// <summary>
        /// Deserializes object
        /// </summary>
        /// <param name="parameter_object"></param>
        /// <returns></returns>
        public static object Get(byte[] parameter_object)
        {
            MemoryStream stream = new MemoryStream(parameter_object);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);          
        }
    }
}
