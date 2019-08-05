using System;
using System.Collections.Generic;

namespace MediaStorage.Common
{
    public interface ILogger
    {
        void Debug(string message, string filePath = "", string methodname = "", int sourceLineNumber = 0, string loggerName = "", IDictionary<string, string> properties = null);
        void Error(string message, Exception ex = null, string filePath = "", string methodname = "", int sourceLineNumber = 0, string loggerName = "", IDictionary<string, string> properties = null);
        void Info(string message, string filePath = "", string methodname = "", int sourceLineNumber = 0, string loggerName = "", IDictionary<string, string> properties = null);
        void Warn(string message, string filePath = "", string methodname = "", int sourceLineNumber = 0, string loggerName = "", IDictionary<string, string> properties = null);
    }
}