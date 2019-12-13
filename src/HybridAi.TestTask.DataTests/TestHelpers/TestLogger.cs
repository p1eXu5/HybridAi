using System.Collections.Generic;
using System.Linq;
using HybridAi.TestTask.ConsoleDbUpdater;

namespace HybridAi.TestTask.DataTests.TestHelpers
{
    internal class TestLogger : ILogger
    {
        private readonly List< string > _messages = new List< string >();

        public void WriteLine( string message )
        {
            _messages.Add( message );
        }

        public string Messages
        {
            get { return _messages.Aggregate( "\n", ( s1, s2 ) => { return $"{s1}\n{s2}"; } ); }
        }
    }

}