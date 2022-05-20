using System;
using System.IO;
using System.Text;

namespace TrainingNotificator.Logger
{
    public class Logger : ILogger
    {
        public void Log(Exception exception)
        {
            var loggedMessage = new StringBuilder();
            loggedMessage.AppendLine(DateTime.Now.ToString());
            loggedMessage.AppendLine("Message: " + exception.Message);
            loggedMessage.AppendLine("Inner ex. message:" + exception.InnerException.Message);
            loggedMessage.AppendLine(new string('=', 20));
            File.AppendAllText(@"D:\test\log.txt", loggedMessage.ToString());
        }
    }
}