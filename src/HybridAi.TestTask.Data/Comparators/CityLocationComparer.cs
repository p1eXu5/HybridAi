﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.Data.Comparators
{
    public class CityLocationComparer : IComparer< CityLocation >, IEqualityComparer< CityLocation >
    {
        public int Compare( [AllowNull] CityLocation x, [AllowNull] CityLocation y )
        {
            if (x == null)
            {
                if (y == null) {
                    return 0;
                }
                else {
                    return -1;
                }
            }
            else {
                if (y == null) {
                    return 1;
                }
                else {
                    return x.GeonameId.CompareTo( y.GeonameId );
                    
                }
            }
        }

        public bool Equals( CityLocation x, CityLocation y )
        {
            return Compare( x, y ) == 0;
        }

        public int GetHashCode( CityLocation obj )
        {
            return obj.GetHashCode();
        }
    }
}
