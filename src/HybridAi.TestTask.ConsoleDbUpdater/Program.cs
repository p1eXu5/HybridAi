using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Extensions;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using System;
using System.Configuration;
using System.IO;
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

            var argumentChainBuilder = new ChainBuilder().AddArgumentFormatter().Append( remoteChainBuilder );



            IChainLink< Request, IResponse< Request > > argumentChain;

            switch ( args.Length ) {
                case 0:
                    IChainLink< Request, IResponse< Request >> defaultChain 
                        = remoteChainBuilder.Build();

                    var remoteResult = defaultChain.Process( new UrlRequest( DEFAULT_FILE_URL ) );

                    if ( remoteResult.Request is DoneRequest done ) {
                        LoggerFactory.Instance.Log( done.Message );
                    }
                    else if ( remoteResult.Request is FailRequest fail ) {
                        Console.WriteLine( "Log:" );
                        LoggerFactory.Instance.Log( fail.Message );
                    }

                    break;
                case 1:
                    throw new NotImplementedException( "Local files is not maintained yet." );
                    argumentChain = argumentChainBuilder.Build();
                    argumentChain.Process( new ArgumentRequest( args[0] ) );
                    break;
                default:
                    throw new NotImplementedException( "Multiple files is not maintained yet." );
                    argumentChain = argumentChainBuilder.Build();
                    var tasks = new Task< IResponse< Request > >[args.Length];
                    for (int i = 0; i < args.Length; i++) {
                        var arg = args[i];
                        tasks[i] = Task.Run( () => argumentChain.Process( new ArgumentRequest( arg ) ) );
                    }
                    IResponse< Request >[] result = await Task.WhenAll( tasks );
                    break;
            }

            Console.ReadKey( true );
        }
    }
}
