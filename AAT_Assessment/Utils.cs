using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AAT_Assessment
{
    public class Utils
    {
        public static void SerializeToBinary(List<int> list, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, list);
            }
        }

        public static void SerializeToXml(List<int> list, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(List<int>));
                serializer.Serialize(stream, list);
            }
        }
    }
}
