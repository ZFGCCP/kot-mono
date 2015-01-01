using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Gears.Cloud.Utility
{
    public static class XMLEngine<T>
    {
        private static XmlSerializer engine;

        public static void SaveToFile(string filePath, T data)
        {
            using (XmlWriter writer = XmlWriter.Create(filePath))
            {
                AddToStream(writer, data);
            }
        }

        public static T LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (XmlReader inputStream = XmlReader.Create(filePath))
                {
                    return ReadFromStream(inputStream);
                }
            }
            return default(T);
        }

        private static T ReadFromStream(XmlReader inputStream)
        {
            if (engine == null)
            {
                engine = new XmlSerializer(typeof(T));
            }
            T fromStream = (T)engine.Deserialize(inputStream);
            return fromStream;
        }

        private static void AddToStream(XmlWriter outputStream, T data)
        {
            if (engine == null)
            {
                engine = new XmlSerializer(typeof(T));
            }
            engine.Serialize(outputStream, data);
        }
    }
}
