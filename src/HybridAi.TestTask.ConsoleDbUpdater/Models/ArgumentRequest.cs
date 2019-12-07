using System;
using System.Collections.Generic;
using System.Text;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class ArgumentRequest : Request
    {
        public ArgumentRequest( string argument )
        {
            Argument = argument;
        }

        public string Argument { get; }
    }
}
