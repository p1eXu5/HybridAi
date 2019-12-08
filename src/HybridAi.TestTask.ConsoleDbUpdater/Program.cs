using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Extensions;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    public class Program
    {
        public const string DEFAULT_FILE_URL = @"https://geolite.maxmind.com/download/geoip/database/GeoLite2-City-CSV.zip";

        static async Task Main(string[] args)
        {
            ChainBuilder remoteChainBuilder = new ChainBuilder().AddDownloader()
                                                                .AddUnzipper();

            ChainBuilder copierChainBuilder = new ChainBuilder().AddUnzipper()
                                                                .AddTempFileCreator();

            ChainBuilder updateChainBuilder = new ChainBuilder().AddExtensionChecker()
                                                                .AddCsvHeaderChecker()
                                                                .AddMapper()
                                                                .AddUpdater();

            var argumentChainBuilder = new ChainBuilder().AddArgumentFormatter()
                                                                  .Append(copierChainBuilder)
                                                                  .Append(updateChainBuilder);

            IChainLink< Request, IResponse< Request > > argumentChain;

            switch ( args.Length ) {
                case 0:
                    IChainLink< Request, IResponse< Request >> defaultChain 
                        = remoteChainBuilder.AddUnzipper().Build();
                    defaultChain.Process( new UrlRequest( DEFAULT_FILE_URL ) );
                    break;
                case 1:
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

        }
    }
}
