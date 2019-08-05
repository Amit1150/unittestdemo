using System;
using System.Collections.Generic;
using System.IO;

namespace MediaStorage.Common
{
    public class Logger : ILogger
    {
        string path = System.Reflection.Assembly.GetEntryAssembly().Location + "log.txt";
        private void Write(string message)
        {

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(message);
                }

            }
            else if (File.Exists(path))
            {
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(message);
                }
            }
        }

        public void Debug(string message, string filePath = "", string methodname = "", int sourceLineNumber = 0, string loggerName = "", IDictionary<string, string> properties = null)
        {
            Write(message);
        }
               
        public void Error(string message, Exception ex = null, string filePath = "", string methodname = "", int sourceLineNumber = 0, string loggerName = "", IDictionary<string, string> properties = null)
        {
            Write(message);
        }

        public void Info(string message, string filePath = "", string methodname = "", int sourceLineNumber = 0, string loggerName = "", IDictionary<string, string> properties = null)
        {
            Write(message);
        }

        public void Warn(string message, string filePath = "", string methodname = "", int sourceLineNumber = 0, string loggerName = "", IDictionary<string, string> properties = null)
        {
            Write(message);
        }
    }
}
