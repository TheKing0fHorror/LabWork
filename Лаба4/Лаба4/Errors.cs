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

        public Error(DateTime dateTime, string source, string exName, string stackTrace)
        {
            this.dateTime = dateTime;
            this.source = source;
            this.exName = exName;
            this.stackTrace = stackTrace;
        }
    }
}
