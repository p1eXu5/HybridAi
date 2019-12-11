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
                         where i.CityLocationGeonameId >= minGeonameId
                                && i.CityLocationGeonameId <= maxGeonameId
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
                        where i.CityLocationGeonameId >= minGeonameId
                            && i.CityLocationGeonameId <= maxGeonameId
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
    }
}
