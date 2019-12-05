using HybridAi.TestTask.ConsoleDbUpdater.Extensions;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var chain = new ChainBuilder().AddDownloader()
                                          .AddUnzipper()
                                          
                                          .Build();

            switch ( args.Length ) {
                case 0:
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
