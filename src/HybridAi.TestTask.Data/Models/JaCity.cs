using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.Data.Models
{
    public class JaCity : City
    {
#nullable enable
        public JaCity( string continentName, string countryName, string? cityName ) : base( continentName, countryName, cityName )
        { }
#nullable restore
    }
}
