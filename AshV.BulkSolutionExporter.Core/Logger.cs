using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AshV.BulkSolutionExporter.Core
{
    public class Logger
    {
        private static readonly Logger instance = new Logger();

        private static string LogFile { get; set; }

        static Logger() { }

        private Logger() { }

        public void Log(string format, params object[] args)
        {
            string log = $"{DateTime.Now} -> {string.Format(format, args)}{Environment.NewLine}";
            Console.Write(log);
            File.AppendAllText(LogFile, log);
        }

        public static Logger GetLogger(string logFileName)
        {
            LogFile = logFileName;
            instance.Log("{2}{0}Logger Instance Created at {1}{0}",
                Environment.NewLine,
                DateTime.Now,
                "-------------------------------------------------------------");
            return instance;
        }
    }
}