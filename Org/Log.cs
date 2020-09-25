using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org
{
    static class Log
    {
        public struct LogMessage
        {
            public LogLevels LogLevel { get; }
            public string Message { get; }
            public DateTime TimeStamp { get; }

            public LogMessage(LogLevels _logLevel, string _message, DateTime _timeStamp)
            {
                LogLevel = _logLevel;
                Message = _message;
                TimeStamp = _timeStamp;
            }
        }

        public enum LogLevels
        {
            Info,
            Warning,
            Error
        }

        public readonly static List<LogMessage> LogMessages = new List<LogMessage>();

        public static void AddLogMessage(LogMessage logMessage)
        {
            LogMessages.Add(logMessage);
        }

        public static void AddLogMessage(LogLevels logLevel, string message)
        {
            LogMessages.Add(new LogMessage(logLevel, message, DateTime.Now));
        }
    }
}
