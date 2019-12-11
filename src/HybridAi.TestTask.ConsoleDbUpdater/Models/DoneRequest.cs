using HybridAi.TestTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class DoneRequest : Request
    {
        public DoneRequest( string message )
        {
            Message = message;
        }

        public string Message { get; }
    }
}
