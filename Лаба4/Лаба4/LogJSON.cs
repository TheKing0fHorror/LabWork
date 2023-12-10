using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Лаба4
{
    internal class LogJSON : ILog
    {
        Error error;
        Errors errors = new Errors();

        public void LogJournal(Exception ex)
        {
            error = new Error(DateTime.UtcNow, ex.Source, ex.GetType().Name, ex.StackTrace);

            // выполняем десериализацию, только если есть файл и он не пустой
            if (File.Exists("errors.json") && !XmlDocument.Equals("errors.json", ""))
                errors = DeserializeJSON("errors.json");
            errors.ErrorsList.Add(error);

            SerializeJSON("errors.json", errors);
        }

        static Errors DeserializeJSON(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<Errors>(fs); ;
            }
        }

        public void SerializeJSON(string fileName, Errors errors)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, errors, options);
            }
        }


    }
}
