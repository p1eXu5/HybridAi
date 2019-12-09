using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ModelMappers
{
    public class CityLocationMapper : ModelMapper
    {
        public static string[] CityLocationHeader { get; } = new[] {
            "geoname_id", // 0
            "locale_code", // 1
            "continent_code", // 2
            "continent_name", // 3
            "country_iso_code", // 4
            "country_name", // 5
            "subdivision_1_iso_code", // 6
            "subdivision_1_name", // 7
            "subdivision_2_iso_code", // 8
            "subdivision_2_name", // 9
            "city_name", // 10
            "metro_code", // 11
            "time_zone", // 12
            "is_in_european_union", // 13
        };

        // ReSharper disable StringLiteralTypo
        public override string[] Header => CityLocationMapper.CityLocationHeader;

        // ReSharper restore StringLiteralTypo

        private delegate ref City CityGetter( CityLocation cityLocation );

        protected override IEntity? _map( string line )
        {
            var values = line.Split( _Splitters );

            if ( values.Length != Header.Length ) return null;

            CityLocation cityLocation = null;

            try {
                cityLocation = new CityLocation( Int32.Parse(values[0]) ) {
                    MetroCode = String.IsNullOrWhiteSpace( values[11] ) ? null : values[11],
                    TimeZone = values[12],
                    IsInEuropeanUnion = Int32.Parse( values[13] ) == 1
                };
            }
            catch ( Exception ex ) {
                LoggerFactory.Instance.Log( ex.Message );
                return null;
            }

            City city = null;


            switch ( values[1].ToLowerInvariant() ) {
                case "en":
                    return _loadCity< EnCity >( values, ref cityLocation, c => cityLocation.EnCity = c );
                case "de":
                    return _loadCity< DeCity >( values, ref cityLocation, c => cityLocation.DeCity = c );
                case "fr":
                    return _loadCity< FrCity >( values, ref cityLocation, c => cityLocation.FrCity = c );
                case "ru":
                    return _loadCity< RuCity >( values, ref cityLocation, c => cityLocation.RuCity = c );
                case "ja":
                    return _loadCity< JaCity >( values, ref cityLocation, c => cityLocation.JaCity = c );
                case "pt-br":
                    return _loadCity< PtBrCity >( values, ref cityLocation, c => cityLocation.PtBrCity = c );
                case "zh-cn":
                    return _loadCity< ZhCnCity >( values, ref cityLocation, c => cityLocation.ZhCnCity = c );
                case "es":
                    return _loadCity< EsCity >( values, ref cityLocation, c => cityLocation.EsCity = c );
                default:
                    return null;
            }
        }

        private CityLocation? _loadCity< T >( string[] values, ref CityLocation cityLocation, Action< T > action )
            where T : City
        {
            T? city = _createCity< T >( values, cityLocation );
            if ( city != null ) {
                city.LocaleCode = new LocaleCode(  values[1] );
                action( city );
                return cityLocation;
            }

            return null;
        }

        private T? _createCity< T >( string[] values, CityLocation cityLocation )
            where T : City
        {
            if ( String.IsNullOrWhiteSpace( values[2] )
                 || String.IsNullOrWhiteSpace( values[5] ) ) {
                return null;
            }

            try {

                var obj = Activator.CreateInstance( typeof( T ), new[] { cityLocation.GeonameId } );

                if ( obj is T city ) 
                {
                    city.ContinentCode = values[2];
                    city.CountryIsoCode = values[4];
                    city.CountryName = values[5];
                    city.Subdivision1IsoCode = String.IsNullOrWhiteSpace( values[6] ) ? null : values[6];
                    city.Subdivision1Name = String.IsNullOrWhiteSpace( values[7] ) ? null : values[7];
                    city.Subdivision2IsoCode = String.IsNullOrWhiteSpace( values[8] ) ? null : values[8];
                    city.Subdivision2Name = String.IsNullOrWhiteSpace( values[9] ) ? null : values[9];
                    city.CityName = values[10];
                    city.CityLocation = cityLocation;
                    return city;
                } 

            }
            catch ( Exception ex ) {
                LoggerFactory.Instance.Log( ex.Message );
            }

            return null;
        }

    }
}
