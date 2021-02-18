using System;

namespace ListApp.Services
{
    public class Log : ILog
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Exception(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception.Message);
        }

        public void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
