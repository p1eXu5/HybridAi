using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class RuCity : City
    {
#nullable enable
        public RuCity( string continentName, string countryName, string? cityName ) : base( continentName, countryName, cityName )
        { }
#nullable restore
    }
}
