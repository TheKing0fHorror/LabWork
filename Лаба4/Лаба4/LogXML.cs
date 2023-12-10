using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Лаба4
{
    internal class LogXML : ILog
    {
        Error error;
        Errors errors = new Errors();

        public void LogJournal(Exception ex)
        {
            error = new Error(DateTime.UtcNow, ex.Source, ex.GetType().Name, ex.StackTrace);

            // выполняем десериализацию, только если есть файл и он не пустой
            if (File.Exists("errors.xml") && !XmlDocument.Equals("errors.xml", ""))
                errors = DeserializeXML("errors.xml");
            errors.ErrorsList.Add(error);

            SerializeXML("errors.xml", errors);
        }

        static Errors DeserializeXML(string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Errors));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return (Errors)xml.Deserialize(fs);
            }
        }

        static void SerializeXML(string fileName, Errors errors)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Errors));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, errors);
            }
        }
    }
}
