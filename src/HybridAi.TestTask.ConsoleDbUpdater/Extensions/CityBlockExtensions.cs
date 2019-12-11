using HybridAi.TestTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HybridAi.TestTask.ConsoleDbUpdater.Extensions
{
    public static class CityBlockExtensions
    {
        public static void CopyCityLocationCity( this CityBlock block, CityLocation cityLocation )
        {
            if ( block.CityLocation == null ) {
                block.CityLocation = cityLocation;
            }
            else {
                var blockCityLocation = block.CityLocation;
                _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.EnCity ) );
                _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.RuCity ) );
                _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.EsCity ) );
                _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.DeCity ) );
                _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.FrCity ) );
                _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.JaCity ) );
                _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.PtBrCity ) );
                _copyCity( blockCityLocation, cityLocation, nameof( blockCityLocation.ZhCnCity ) );
            }
        }

        private static void _copyCity( CityLocation clkA, CityLocation clkB, string propertyName )
        {
            PropertyInfo? clkAPropertyInfo = clkA.GetType().GetProperty( propertyName, BindingFlags.Instance | BindingFlags.Public );
            PropertyInfo? clkBPropertyInfo = clkB.GetType().GetProperty( propertyName, BindingFlags.Instance | BindingFlags.Public );

            if ( clkAPropertyInfo != null && clkBPropertyInfo != null ) {

                object? valB = clkBPropertyInfo.GetValue( clkB );

                if ( clkAPropertyInfo.GetValue( clkA ) == null && valB != null ) {
                    clkAPropertyInfo.SetValue( clkA, valB );
                }

            }
        }
    }
}
