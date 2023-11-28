using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Лаба4
{
    [Serializable]
    public class Errors
    {
        private List<Error> errors = new List<Error>();
        public List<Error> ErrorsList { get => errors; set => errors = value; }
    }

    [Serializable]
    public class Error
    {
        private Errors errors = new Errors();
        private DateTime dateTime; // дата создания
        private string source; // имя приложения или объекта, вызвавшего ошибку
        private string exName; // имя ошибки
        private string stackTrace; // трэйслог ошибки


        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public string Source { get => source; set => source = value; }
        public string ExName { get => exName; set => exName = value; }
        public string StackTrace { get => stackTrace; set => stackTrace = value; }

        public Error()
        {

        }

        public Error(DateTime dateTime, string source, string name, string stackTrace)
        {
            this.dateTime = dateTime;
            this.source = source;
            this.exName = name;
            this.stackTrace = stackTrace;
        }

        public void LogJournal(Exception ex)
        {
            // выполняем сбор данных об ошибке
            DateTime = DateTime.Now;
            Source = ex.Source;
            ExName = ex.GetType().Name;
            StackTrace = ex.StackTrace;

            // записываем информацию об ошибке в файл
            StreamWriter sw = new StreamWriter("LogJournal.txt", true);
            sw.WriteLine($"{dateTime}##$#$$$$$#$##{source}##$#$$$$$#$##{exName}##$#$$$$$#$##{stackTrace}" + "\n");
            sw.Close();

            // выполняем десериализацию, только если есть файл и он не пустой
            if(File.Exists("error.xml") && XmlDocument.Equals("error.xml",""))
                errors = DeserializeXML("error.xml");
            errors.ErrorsList.Add(new Error(dateTime, source, exName, stackTrace));

            SerializeJSON("error.json", errors);

            SerializeXML("error.xml", errors);
        }

        static void SerializeXML(string fileName, Errors errors)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Errors));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, errors);
            }
        }

        static Errors DeserializeXML(string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Errors));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return (Errors)xml.Deserialize(fs);
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
