using HybridAi.TestTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class ModelRequest< TModel > : Request
        where TModel : IEntity
    {
    }
}
