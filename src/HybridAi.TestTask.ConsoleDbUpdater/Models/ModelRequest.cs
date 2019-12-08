﻿using HybridAi.TestTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class ModelRequest< TModel > : Request
        where TModel : IEntity
    {
        public ModelRequest( ICollection< TModel > collection )
        {
            Collection = collection;
        }

        public ICollection< TModel > Collection { get; }
    }
}
