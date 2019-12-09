using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class FrCity : City
    {
#nullable enable
        public FrCity( string continentName, string countryName, string? cityName ) : base( continentName, countryName, cityName )
        { }
#nullable restore
    }
}
