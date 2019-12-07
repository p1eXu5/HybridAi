using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;
using HybridAi.TestTask.ConsoleDbUpdater.Extensions;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    class Program
    {
        private const string DEFAULT_FILE_URL = @"https://geolite.maxmind.com/download/geoip/database/GeoLite2-City-CSV.zip";

        static async Task Main(string[] args)
        {
            ChainBuilder remoteChainBuilder = new ChainBuilder().AddDownloader();

            ChainBuilder copierChainBuilder = new ChainBuilder().AddUnzipper()
                                                         .AddTempFileCreator();

            ChainBuilder updateChainBuilder = new ChainBuilder().AddExtensionChecker()
                                                         .AddCsvHeaderChecker()
                                                         .AddMapper()
                                                         .AddUpdater();

            var argumentChainBuilder = new ChainBuilder().AddArgumentFormatter()
                                                                  .Append(copierChainBuilder)
                                                                  .Append(updateChainBuilder);

            IChainLink< Request, Response> argumentChain;

            switch ( args.Length ) {
                case 0:
                    IChainLink< Request, Response> defaultChain = remoteChainBuilder.Append( copierChainBuilder ).Append(updateChainBuilder).Build();
                    defaultChain.Process( new UrlRequest( DEFAULT_FILE_URL ) );
                    break;
                case 1:
                    argumentChain = argumentChainBuilder.Build();
                    argumentChain.Process( new ArgumentRequest( args[0] ) );
                    break;
                default:
                    argumentChain = argumentChainBuilder.Build();
                    var tasks = new Task< Response >[args.Length];
                    for (int i = 0; i < args.Length; i++) {
                        var arg = args[i];
                        tasks[i] = Task.Run( () => argumentChain.Process( new ArgumentRequest( arg ) ) );
                    }
                    Response[] result = await Task.WhenAll( tasks );
                    break;
            }

        }
    }
}
