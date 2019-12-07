﻿using System;
using System.Collections.Generic;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class ArgumentFormatter : ChainLink
    {
        public ArgumentFormatter( IChainLink< Request, Response > successor ) 
            : base( successor )
        { }
    }
}
