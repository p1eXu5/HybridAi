using HybridAi.TestTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Comparators
{
    public class CityComparer : IComparer< City >, IEqualityComparer< City >
    {
        public int Compare( [AllowNull] City x, [AllowNull] City y )
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

        public bool Equals( City x, City y )
        {
            return Compare( x, y ) == 0;
        }

        public int GetHashCode( City obj )
        {
            return obj.GetHashCode();
        }
    }
}
