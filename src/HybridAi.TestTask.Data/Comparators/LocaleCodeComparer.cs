using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.Data.Comparators
{
    public class LocaleCodeComparer : IComparer< LocaleCode >, IEqualityComparer< LocaleCode >
    {
        public int Compare( [AllowNull] LocaleCode x, [AllowNull] LocaleCode y )
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
                    return String.Compare( x.Name, y.Name, StringComparison.Ordinal );
                }
            }
        }

        public bool Equals( LocaleCode x, LocaleCode y )
        {
            return Compare( x, y ) == 0;
        }

        public int GetHashCode( LocaleCode obj )
        {
            return obj.GetHashCode();
        }
    }
}
