﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ModelMappers
{
    public class CityBlockMapper : ModelMapper
    {
        // ReSharper disable StringLiteralTypo
        public static string[] CityBlockHeader { get; } = new[] {
            "network",
            "geoname_id",
            "registered_country_geoname_id",
            "represented_country_geoname_id",
            "is_anonymous_proxy",
            "is_satellite_provider",
            "postal_code",
            "latitude",
            "longitude",
            "accuracy_radius"
        };
        // ReSharper restore StringLiteralTypo

        public override string[] Header => CityBlockMapper.CityBlockHeader;

        protected override IEntity? _map( string line )
        {
            var values = line.Split( _Splitters );

            if ( values.Length != Header.Length ) return null;
            if ( values[0].Contains( ':' ) ) {
                return _createCityBlock< CityBlockIpv6 >( values );
            }
            else {
                return _createCityBlock< CityBlockIpv4 >( values );
            }
        }

        private T? _createCityBlock< T >( string[] values )
            where T : CityBlock
        {
            try {
                var obj = Activator.CreateInstance( typeof( T ), new[] { values[0] } );

                if ( obj is T block ) 
                {
                    block.CityLocationGeonameId = String.IsNullOrWhiteSpace( values[1] ) ? (int?)null : int.Parse( values[1] );
                    block.RegistredCountryGeonameId = String.IsNullOrWhiteSpace( values[2] ) ? (int?)null : int.Parse( values[2] );
                    // no data in the example file
                    block.RepresentedCountryGeonameId = null;
                    block.IsAnonymousProxy = int.Parse( values[4] ) == 1;
                    block.IsSatelliteProvider = int.Parse( values[5] ) == 1;
                    block.PostalCode = String.IsNullOrWhiteSpace( values[6] ) ? null : values[6];
                    block.Latitude = String.IsNullOrWhiteSpace( values[7] ) ? (double?)null : double.Parse( values[7] );
                    block.Longitude = String.IsNullOrWhiteSpace( values[8] ) ? (double?)null : double.Parse( values[8] );
                    block.AccuracyRadius = String.IsNullOrWhiteSpace( values[9] ) ? (short?)null : short.Parse( values[9] );

                    return block;
                }
            }
            catch ( Exception ex ) {
                LoggerFactory.Instance.Log( ex.Message + " " + String.Join( ',', values ) );
            }

            return null;
        }
    }
}
