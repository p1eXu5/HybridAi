using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.Data.Extensions;
using HybridAi.TestTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HybridAi.TestTask.Data.Services.WebApi
{
    public static class IpDbContextExtensions
    {

        public static async Task< CityBlockIpv4 > GetCityBlockIpv4( this IpDbContext context, IPAddress address )
        {
            var ipBytes = address.GetAddressBytes();
            var minNetwork = BitConverter.ToString(ipBytes.Decrement());
            var maxNetwork = BitConverter.ToString(ipBytes.Increment());

            var command = $"SELECT * FROM dbo.\"CityBlocksIpv4s\" AS block WHERE '{minNetwork}' <= block.\"Network\" AND block.\"Network\" <= '{maxNetwork}'";
            var ip4s = await context.CityBlockIpv4Collection.FromSqlRaw( command )
                                           .Include( b => b.CityLocation )
                                           .ThenInclude( cl => cl.EnCity )
                                           .Include( b => b.CountryLocation )
                                           .ThenInclude( cl => cl.EnCity )
                                           .OrderByDescending( b => b.Network )
                                           .ToArrayAsync();
            if ( ip4s.Any() ) {
                var ipString = BitConverter.ToString( ipBytes );
                var input = ipString.ToBigInteger();
                return ip4s.First( b => b.Network.ToBigInteger() <= input );
            }
            return null;
        }


        public static async Task< CityBlockIpv6 > GetCityBlockIpv6( this IpDbContext context, IPAddress address )
        {
            var ipBytes = address.GetAddressBytes();
            var minNetwork = BitConverter.ToString(ipBytes.Decrement());
            var maxNetwork = BitConverter.ToString(ipBytes.Increment());

            var command = $"SELECT * FROM dbo.\"CityBlocksIpv6s\" AS block WHERE '{minNetwork}' <= block.\"Network\" AND block.\"Network\" <= '{maxNetwork}'";
            var ip6s = await context.CityBlockIpv6Collection.FromSqlRaw( command )
                                           .Include( b => b.CityLocation )
                                           .ThenInclude( cl => cl.EnCity )
                                           .Include( b => b.CountryLocation )
                                           .ThenInclude( cl => cl.EnCity )
                                           .OrderByDescending( b => b.Network )
                                           .ToArrayAsync();
            if ( ip6s.Any() ) {

                var ipString = BitConverter.ToString( ipBytes );
                var input = ipString.ToBigInteger();
                return ip6s.First( b => b.Network.ToBigInteger() <= input );
            }

            return null;
        }

        public static byte[] Decrement( this byte[] ipBytes )
        {
            var ob = new byte[ipBytes.Length];
            ipBytes.CopyTo( ob, 0 );

            for (int i = 0; i < ob.Length; i++) {
                if ( ob[i] > 0 ) {
                    --ob[i];
                    return ob;
                }
            }

            return ob;
        }

        public static byte[] Increment( this byte[] ipBytes )
        {
            var ob = new byte[ipBytes.Length];
            ipBytes.CopyTo( ob, 0 );

            for (int i = 0; i < ob.Length; i++) {
                if ( ob[i] < 255 ) {
                    ++ob[i];
                    return ob;
                }
            }

            return ob;
        }
    }
}
