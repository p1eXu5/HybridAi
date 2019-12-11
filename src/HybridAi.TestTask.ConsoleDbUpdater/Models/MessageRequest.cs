﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class MessageRequest : Request
    {
        public MessageRequest( string message )
        {
            Message = message;
        }

        public string Message { get; }
    }
}
