using HybridAi.TestTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Comparators
{
    public class CityBlockComparer : IComparer<CityBlock>
    {
        public int Compare( [AllowNull] CityBlock x, [AllowNull] CityBlock y )
        {
            if (x?.CityLocationGeonameId == null )
            {
                if (y?.CityLocationGeonameId == null) {
                    return 0;
                }
                else {
                    return -1;
                }
            }
            else {
                if (y?.CityLocationGeonameId == null) {
                    return 1;
                }
                else {

                    return x.CityLocationGeonameId.Value.CompareTo( y.CityLocationGeonameId.Value );
                    
                }
            }
        }
    }
}
