﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class DeCity : City
    {
#nullable enable
        public DeCity( int geonameId ) : base( geonameId )
        { }
#nullable restore
    }
}
