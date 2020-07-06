using System;
using System.IO;
using log4net;
using log4net.Config;

namespace RozetkaTesting.helpers
{
    public static class LoggerHelper
    {
        private const string DefaultConfigFilename = "log4net.config";
        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");


        public static void InitLogger()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultConfigFilename);
            XmlConfigurator.Configure(new FileInfo(filePath));
            Log.Info("Log initialize succeed");
        }
    }
}