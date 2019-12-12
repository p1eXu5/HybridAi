using HybridAi.TestTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace HybridAi.TestTask.ConsoleDbUpdater.Extensions
{
    public static class CityBlockExtensions
    {
        public static IEnumerable< City > CopyCityLocationCity( this CityBlock block, CityLocation cityLocation )
        {
            var res = new List< City? >(8);

            if ( block.CityLocation == null ) {
                block.CityLocation = cityLocation;
                res.Add( cityLocation.EnCity );
                res.Add( cityLocation.RuCity );
                res.Add( cityLocation.EsCity );
                res.Add( cityLocation.DeCity );
                res.Add( cityLocation.FrCity );
                res.Add( cityLocation.JaCity );
                res.Add( cityLocation.PtBrCity );
                res.Add( cityLocation.ZhCnCity );
            }
            else {
                var blockCityLocation = block.CityLocation;
                res.Add( _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.EnCity ) ) );
                res.Add( _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.RuCity ) ) );
                res.Add( _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.EsCity ) ) );
                res.Add( _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.DeCity ) ) );
                res.Add( _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.FrCity ) ) );
                res.Add( _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.JaCity ) ) );
                res.Add( _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.PtBrCity ) ) );
                res.Add( _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.ZhCnCity ) ) );
            }

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return res.Where( c => c != null );
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        private static City? _copyCity( CityLocation clkA, CityLocation clkB, string propertyName )
        {
            PropertyInfo? clkAPropertyInfo = clkA.GetType().GetProperty( propertyName, BindingFlags.Instance | BindingFlags.Public );
            PropertyInfo? clkBPropertyInfo = clkB.GetType().GetProperty( propertyName, BindingFlags.Instance | BindingFlags.Public );

            if ( clkAPropertyInfo != null && clkBPropertyInfo != null ) {

                object? valB = clkBPropertyInfo.GetValue( clkB );

                if ( clkAPropertyInfo.GetValue( clkA ) == null && valB != null ) {
                    clkAPropertyInfo.SetValue( clkA, valB );
                }

                return (City?)valB;
            }

            return null;
        }
    }
}
