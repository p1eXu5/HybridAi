using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    public class LoggerFactory
    {
        private static LoggerFactory _instance;

        public static LoggerFactory Instance
            => _instance ??= new LoggerFactory();

        protected LoggerFactory() { }

        private ILogger? _logger;

        public ILogger Logger
        {
            get { return _logger ??= new ConsoleLogger(); }
            set => _logger = value;
        }

        public void Log( string msg )
        {
            Logger.WriteLine( msg );
        }
    }
}
