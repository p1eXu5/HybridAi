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
                res.Add( CopyProperty<CityLocation, City>( blockCityLocation, cityLocation, nameof( blockCityLocation.EnCity ) ) );
                res.Add( CopyProperty<CityLocation, City>( blockCityLocation, cityLocation, nameof( blockCityLocation.RuCity ) ) );
                res.Add( CopyProperty<CityLocation, City>( blockCityLocation, cityLocation, nameof( blockCityLocation.EsCity ) ) );
                res.Add( CopyProperty<CityLocation, City>( blockCityLocation, cityLocation, nameof( blockCityLocation.DeCity ) ) );
                res.Add( CopyProperty<CityLocation, City>( blockCityLocation, cityLocation, nameof( blockCityLocation.FrCity ) ) );
                res.Add( CopyProperty<CityLocation, City>( blockCityLocation, cityLocation, nameof( blockCityLocation.JaCity ) ) );
                res.Add( CopyProperty<CityLocation, City>( blockCityLocation, cityLocation, nameof( blockCityLocation.PtBrCity ) ) );
                res.Add( CopyProperty<CityLocation, City>( blockCityLocation, cityLocation, nameof( blockCityLocation.ZhCnCity ) ) );
            }

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return res.Where( c => c != null );
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }



        public static TOut CopyProperty<TIn, TOut>( TIn clkA, TIn clkB, string propertyName )
            where TIn : IEntity
        {
            PropertyInfo? clkAPropertyInfo = clkA.GetType().GetProperty( propertyName, BindingFlags.Instance | BindingFlags.Public );
            PropertyInfo? clkBPropertyInfo = clkB.GetType().GetProperty( propertyName, BindingFlags.Instance | BindingFlags.Public );

            if ( clkAPropertyInfo != null && clkBPropertyInfo != null ) {

                object? valB = clkBPropertyInfo.GetValue( clkB );

                if ( clkAPropertyInfo.GetValue( clkA ) == null && valB != null ) {
                    clkAPropertyInfo.SetValue( clkA, valB );
                }

                return (TOut)valB;
            }

            return default(TOut);
        }

        /// <summary>
        /// Indexes must be sorted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public static List<T> RemoveAtIndexes<T>( this List<T> input, List< int > indexes )
            where T : IEntity
        {
            if ( !indexes.Any() ) return input;

            var ii = 0;

            for (int i = 0; i < input.Count; i++) {
                
                if ( indexes[ii] - ii == i ) {
                    input.RemoveAt( i );
                    --i;
                    ++ii;
                }

                if ( ii >= indexes.Count ) return input;
            }

            return input;
        }
    }
}
