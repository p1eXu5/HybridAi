using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Models;

namespace HybridAi.TestTask.WebApi.Models
{
    public class CityLocationInfo
    {

        public string Ip { get; set; }
        public bool IsAnonymousProxy { get; set; }
        public bool IsSatelliteProvider { get; set; }

        public string PostalCode { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public short? AccuracyRadius { get; set; }


        public string ContinentCode { get; set; }

        public string CountryIsoCode { get; set; }

        public string Subdivision1IsoCode { get; set; }

        public string? Subdivision2IsoCode { get; set; }

        public string? MetroCode { get; set; }
        
        public string TimeZone { get; set; }

        public bool IsInEuropeanUnion { get; set; }



        public string ContinentName { get; set; }

        public string CountryName { get; set; }

        public string Subdivision1Name { get; set; }

        public string Subdivision2Name { get; set; }

        public string CityName { get; set; }



        public static explicit operator CityLocationInfo ( CityBlock block )
        {

            var res = new CityLocationInfo() {
                IsAnonymousProxy = block.IsAnonymousProxy,
                IsSatelliteProvider = block.IsSatelliteProvider,
                PostalCode = block.PostalCode,
                Latitude = block.Latitude,
                Longitude = block.Longitude,
                AccuracyRadius = block.AccuracyRadius,
            };

            CityLocation cityLocation = null;

            if ( block.CityLocation != null ) {
                cityLocation = block.CityLocation;
                res.ContinentCode = cityLocation.ContinentCode;
                res.CountryIsoCode = cityLocation.CountryIsoCode;
                res.Subdivision1IsoCode = cityLocation.Subdivision1IsoCode;
                res.Subdivision2IsoCode = cityLocation.Subdivision2IsoCode;
                res.MetroCode = cityLocation.MetroCode;
                res.TimeZone = cityLocation.TimeZone;
                res.IsInEuropeanUnion = cityLocation.IsInEuropeanUnion;
            }
            else if ( block.CountryLocation != null ) {
                cityLocation = block.CountryLocation;
                res.ContinentCode = cityLocation.ContinentCode;
                res.CountryIsoCode = cityLocation.CountryIsoCode;
                res.Subdivision1IsoCode = cityLocation.Subdivision1IsoCode;
                res.Subdivision2IsoCode = cityLocation.Subdivision2IsoCode;
                res.MetroCode = cityLocation.MetroCode;
                res.TimeZone = cityLocation.TimeZone;
                res.IsInEuropeanUnion = cityLocation.IsInEuropeanUnion;
            }

            if ( cityLocation?.EnCity != null ) {
                var city = cityLocation.EnCity;
                res.ContinentName = city.ContinentName;
                res.CountryName = city.CountryName;
                res.Subdivision1Name = city.Subdivision1Name;
                res.Subdivision2Name = city.Subdivision2Name;
                res.CityName = city.CityName;
            }

            return res;
        }
    }
}
