﻿using System;
using System.Collections.Generic;
using System.Text;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public interface IChainLink< in TIn, out TOut >
        where TIn : Request 
        where TOut : Request
    {
        TOut Process( TIn request );
    }
}