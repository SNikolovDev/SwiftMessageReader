using NLog;

namespace SwiftMessageReader.Helpers
{
    public static class SwiftLogger
    {
        private static readonly Logger logger = LogManager.GetLogger("SwiftLogger");

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
        }
    }
}