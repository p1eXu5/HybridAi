using System;
using System.Collections.Generic;
using System.Text;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    class ArgumentRequest : Request
    {
        private string _argument;

        public ArgumentRequest( string argument )
        {
            _argument = argument;
        }
    }
}
