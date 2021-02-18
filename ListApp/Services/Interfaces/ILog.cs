using System;

namespace ListApp.Services
{
    public interface ILog
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Exception(Exception exception);
    }
}
