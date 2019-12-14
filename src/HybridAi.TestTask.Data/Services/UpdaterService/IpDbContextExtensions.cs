using HybridAi.TestTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HybridAi.TestTask.Data.Comparators;

namespace HybridAi.TestTask.Data.Services.UpdaterService
{
    public static class IpDbContextExtensions
    {
        public static List< CityBlock > GetCityBlocks( this IpDbContext context, int minGeonameId, int maxGeonameId )
        {
            List< CityBlock > ip = ( from i in context.CityBlockIpv4Collection
                         where (i.CityLocationGeonameId >= minGeonameId
                                && i.CityLocationGeonameId <= maxGeonameId)
                               || i.CityLocationGeonameId == 0
                         orderby i.CityLocationGeonameId
                         select i).Include( b => b.CityLocation)
                                  .ThenInclude( c => c.EnCity)
                                  .Include( b => b.CityLocation)
                                  .ThenInclude( c => c.RuCity )
                                  .Include( b => b.CityLocation)
                                  .ThenInclude( c => c.DeCity )
                                  .Include( b => b.CityLocation)
                                  .ThenInclude( c => c.FrCity )
                                  .Include( b => b.CityLocation)
                                  .ThenInclude( c => c.EsCity )
                                  .Include( b => b.CityLocation)
                                  .ThenInclude( c => c.JaCity )
                                  .Include( b => b.CityLocation)
                                  .ThenInclude( c => c.PtBrCity )
                                  .Include( b => b.CityLocation)
                                  .ThenInclude( c => c.ZhCnCity )
                                  .Cast< CityBlock >().ToList();
            ip.AddRange(
                
                ( from i in context.CityBlockIpv6Collection
                        where (i.CityLocationGeonameId >= minGeonameId
                               && i.CityLocationGeonameId <= maxGeonameId)
                              || i.CityLocationGeonameId == 0
                        orderby i.CityLocationGeonameId
                        select i).Include( b => b.CityLocation)
                                .ThenInclude( c => c.EnCity)
                                .Include( b => b.CityLocation)
                                .ThenInclude( c => c.RuCity )
                                .Include( b => b.CityLocation)
                                .ThenInclude( c => c.DeCity )
                                .Include( b => b.CityLocation)
                                .ThenInclude( c => c.FrCity )
                                .Include( b => b.CityLocation)
                                .ThenInclude( c => c.EsCity )
                                .Include( b => b.CityLocation)
                                .ThenInclude( c => c.JaCity )
                                .Include( b => b.CityLocation)
                                .ThenInclude( c => c.PtBrCity )
                                .Include( b => b.CityLocation)
                                .ThenInclude( c => c.ZhCnCity )
                                .Cast< CityBlock >().ToList()
            );

            ip.Sort( new CityBlockComparer() );

            return ip;
        }


        public static IQueryable< CityLocation > GetCityLocations( this IpDbContext context, int minGeonameId, int maxGeonameId )
        {
            return
                ( from i in context.CityLocations
                  where i.GeonameId >= minGeonameId
                      && i.GeonameId <= maxGeonameId
                  orderby i.GeonameId
                  select i).Include( c => c.RuCity )
                           .Include( c => c.DeCity )
                           .Include( c => c.FrCity )
                           .Include( c => c.EsCity )
                           .Include( c => c.JaCity )
                           .Include( c => c.PtBrCity )
                           .Include( c => c.ZhCnCity )
                           .AsQueryable();
        }

        /// <summary>
        /// Returns <see cref="IQueryable"/> of <see cref="CityBlockIpv4"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IQueryable< CityBlockIpv4 > GetIpv4Blocks( this IpDbContext context )
        {
            return context.CityBlockIpv4Collection.AsQueryable();
        }

        /// <summary>
        /// Returns <see cref="IQueryable"/> of <see cref="CityBlockIpv6"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IQueryable< CityBlockIpv6 > GetIpv6Blocks( this IpDbContext context )
        {
            return context.CityBlockIpv6Collection.AsQueryable();
        }

        /// <summary>
        /// Returns <see cref="IQueryable"/> of <see cref="CityLocation"/> ordered by <see cref="CityLocation.GeonameId"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IQueryable< CityLocation > GetCityLocations( this IpDbContext context )
        {
            return context.CityLocations.OrderBy( c => c.GeonameId ).AsQueryable();
        }

        /// <summary>
        /// Returns <see cref="IQueryable"/> of <see cref="EnCity"/> ordered by <see cref="City.GeonameId"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IQueryable< EnCity > GetEnCities( this IpDbContext context )
        {
            return context.EnCities.OrderBy( c => c.GeonameId ).AsQueryable();
        }

        /// <summary>
        /// Returns <see cref="IQueryable"/> of <see cref="EsCity"/> ordered by <see cref="City.GeonameId"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IQueryable< EsCity > GetEsCities( this IpDbContext context )
        {
            return context.EsCities.OrderBy( c => c.GeonameId ).AsQueryable();
        }

        public static City? GetCity( this IpDbContext context, City city )
            => city switch {
                EnCity en => context.EnCities.FirstOrDefault( c => c.GeonameId == en.GeonameId ),
                RuCity ru => context.RuCities.FirstOrDefault( c => c.GeonameId == ru.GeonameId ),
                FrCity fr => context.FrCities.FirstOrDefault( c => c.GeonameId == fr.GeonameId ),
                DeCity de => context.DeCities.FirstOrDefault( c => c.GeonameId == de.GeonameId ),
                EsCity es => context.EsCities.FirstOrDefault( c => c.GeonameId == es.GeonameId ),
                JaCity ja => context.JaCities.FirstOrDefault( c => c.GeonameId == ja.GeonameId ),
                PtBrCity pt => context.PtBrCities.FirstOrDefault( c => c.GeonameId == pt.GeonameId ),
                ZhCnCity zh => context.ZhCnCities.FirstOrDefault( c => c.GeonameId == zh.GeonameId ),
                _ => null
            };

        public static IQueryable< EnCity > GetEnCities( this IpDbContext context, int minGeonameId, int maxGeonameId )
        {
            return context.EnCities.Where( c => minGeonameId <= c.GeonameId && c.GeonameId <= maxGeonameId ).AsQueryable();
        }

        public static IQueryable< CityBlockIpv4 > GetCityBlockIpv4s( this IpDbContext context, string minNetwork, string maxNetwork )
        {
            var command = $"SELECT * FROM dbo.\"CityBlocksIpv4s\" AS block WHERE '{minNetwork}' <= block.\"Network\" AND block.\"Network\" <= '{maxNetwork}' ORDER BY block.\"Network\";";
            return context.CityBlockIpv4Collection.FromSqlRaw( command );
        }

        public static IQueryable< CityBlockIpv6 > GetCityBlockIpv6s( this IpDbContext context, string minNetwork, string maxNetwork )
        {
            var command = $"SELECT * FROM dbo.\"CityBlocksIpv6s\" AS block WHERE '{minNetwork}' <= block.\"Network\" AND block.\"Network\" <= '{maxNetwork}' ORDER BY block.\"Network\";";
            return context.CityBlockIpv6Collection.FromSqlRaw( command );
        }
    }
}
