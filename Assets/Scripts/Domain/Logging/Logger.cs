using Planetoid.Logging;
using System;

namespace Planetoid.Logging {
    public class Logger
    {
        private static Logger instance;
        private ILoggingProvider logProvider;

        private Logger() { }

        public static Logger Instance()
        {
            if (instance == null) { instance = new Logger(); }

            return instance;
        }

        public void AttachLoggingProvider(ILoggingProvider logProvider)
        {
            this.logProvider = logProvider;
        }

        public void Log(string msg)
        {
            if (logProvider == null) throw new System.Exception("No LoggingProvider provided");

            this.logProvider.Log(msg);
        }
    }
}