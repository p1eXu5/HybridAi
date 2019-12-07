using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Extensions
{
    public static class ChainBuilderExtensions
    {
        public static ChainBuilder AddArgumentFormatter( this ChainBuilder builder )
        {
            return builder;
        }

        public static ChainBuilder AddDownloader(this ChainBuilder builder)
        {
            return builder;
        }

        public static ChainBuilder AddUnzipper(this ChainBuilder builder)
        {
            return builder;
        }

        public static ChainBuilder AddExtensionChecker(this ChainBuilder builder)
        {
            return builder;
        }

        public static ChainBuilder AddCsvHeaderChecker(this ChainBuilder builder)
        {
            return builder;
        }

        public static ChainBuilder AddMapper(this ChainBuilder builder)
        {
            return builder;
        }

        public static ChainBuilder AddUpdater(this ChainBuilder builder)
        {
            return builder;
        }

        public static ChainBuilder AddTempFileCreator(this ChainBuilder builder)
        {
            return builder;
        }
    }
}
