using System;
using System.Collections.Generic;
using System.Text;

namespace HybridAi.TestTask.ConsoleDbUpdater.Tests.TestHelpers
{
    public static class TestLoggerExtensions
    {
        public static string GetMessages( this ILogger logger )
        {
            return ((TestLogger)logger).Messages;
        }
    }
}
