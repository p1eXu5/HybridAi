/*
 * This product includes GeoLite2 data created by MaxMind, available from
 * <a href="https://www.maxmind.com">https://www.maxmind.com</a>.
 */


using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Extensions;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using System;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ModelMappers;
using HybridAi.TestTask.Data;
using Microsoft.EntityFrameworkCore;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    public class Program
    {
        public const string DEFAULT_FILE_URL = @"https://geolite.maxmind.com/download/geoip/database/GeoLite2-City-CSV.zip";

        static async Task Main(string[] args)
        {
            var options = DbContextOptionsFactory.Instance.DbContextOptions;
            // ReSharper disable once UseAwaitUsing
            using ( var ctx = new IpDbContext(options) )
            {
                try {
                    ctx.Database.Migrate();
                }
                catch ( Exception ex ) {
                    Console.WriteLine( ex.Message );
                    return;
                }
            }

            ModelMapperFactory.Instance.Register< CityBlockMapper >( CityBlockMapper.CityBlockHeader );
            ModelMapperFactory.Instance.Register< CityLocationMapper >( CityLocationMapper.CityLocationHeader );

            ChainBuilder remoteChainBuilder = new ChainBuilder().AddDownloader()
                                                                .AddUnzipper()
                                                                .AddMapper()
                                                                .AddUpdater();;

            switch ( args.Length ) {
                case 0:
                    goto l1;
                    IChainLink< Request, IResponse< Request >> remote 
                        = remoteChainBuilder.Build();

                    var remoteResult = remote.Process( new UrlRequest( DEFAULT_FILE_URL ) );
                    CheckLog( remoteResult );
                    
                    break;
                case 1:
l1:
                    var argumentChainBuilder = new ChainBuilder().AddArgumentFormatter()
                                                                 .Append( remoteChainBuilder );
                    var argumentChain = argumentChainBuilder.Build();
                    var argumentResult = argumentChain.Process( new ArgumentRequest(
                        // TODO: change to args[0] when release
                        // @"D:\Projects\Programming Projects\C# Projects\web\HybridAi\docs\GeoLite2-City-CSV_20191126\GeoLite2-City-CSV-slim.zip"
                        @"D:\Projects\Programming Projects\C# Projects\web\HybridAi\docs\GeoLite2-City-CSV_20191210.zip" 
                    ) );

                    CheckLog( argumentResult );

                    break;
                default:
                    throw new NotImplementedException( "Multiple files is not maintained yet." );

#pragma warning disable CS0162 // Unreachable code detected
                    argumentChain = argumentChainBuilder.Build();
                    var tasks = new Task< IResponse< Request > >[args.Length];
                    for (int i = 0; i < args.Length; i++) {
                        var arg = args[i];
                        tasks[i] = Task.Run( () => argumentChain.Process( new ArgumentRequest( arg ) ) );
                    }
                    IResponse< Request >[] result = await Task.WhenAll( tasks );
                    break;
#pragma warning restore CS0162 // Unreachable code detected
            }

            Console.ReadKey( true );
        }

        static void CheckLog( IResponse< Request > response )
        {
            if ( response.Request is DoneRequest done ) {
                LoggerFactory.Instance.Log( done.Message );
            }
            else if ( response.Request is FailRequest fail ) {
                Console.WriteLine( "Log:" );
                LoggerFactory.Instance.Log( fail.Message );
            }
        }
    }
}
