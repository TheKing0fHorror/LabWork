using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Лаба4
{
    internal class LogTXT : ILog
    {
        public void LogJournal(Exception ex)
        {
            Error error;

            // выполняем сбор данных об ошибке
            error = new Error(DateTime.UtcNow, ex.Source, ex.GetType().Name, ex.StackTrace);

            // записываем информацию об ошибке в файл
            StreamWriter sw = new StreamWriter("LogJournal.txt", true);
            sw.WriteLine($"{error.DateTime}##$#$$$$$#$##{error.Source}##$#$$$$$#$##{error.ExName}##$#$$$$$#$##{error.StackTrace}" + "\n");
            sw.Close();
        }
    }
}
