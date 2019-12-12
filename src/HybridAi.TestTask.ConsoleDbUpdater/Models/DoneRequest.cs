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

        public DoneRequest( int newCount, int updateCount, string message )
        {
            NewCount = newCount;
            UpdateCount = updateCount;
            Message = message;
        }


        public int NewCount { get; set; } = -1;

        public int UpdateCount { get; set; } = -1;

        public string Message { get; }
    }
}
