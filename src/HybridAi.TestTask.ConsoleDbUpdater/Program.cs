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
            IChainLink chain = new ChainBuilder().AddDownloader()
                                                 .AddUnzipper()
                                                 .Build();

            IChainLink updateChain = new ChainBuilder.AddExtensionChecker()
                                                    .CsvHeaderChecker()
                                                    .AddSegmenter()
                                                    .AddMapper()
                                                    .AddUpdater()
                                                    .AddDbStorer();

            switch ( args.Length ) {
                case 0:
                    var response = chain.Process( new UrlRequest( DEFAULT_FILE_URL ) ) as FileCollectionResponse;
                    Run();
                    break;
                case 1:
                    break;
                default:
                    var tasks = new Task<Response>[args.Length];
                    for (int i = 0; i < args.Length; i++) {
                        var file = args[i];
                        tasks[i] = Task.Run( () => chain.Process( new ArgumentRequest( file ) ) );
                    }
                    Response[] result = await Task.WhenAll( tasks );
                    break;
            }

        }
    }
}
