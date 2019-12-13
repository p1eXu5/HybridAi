using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.ChainLinks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Extensions
{
    public static class ChainBuilderExtensions
    {
        public static ChainBuilder AddArgumentFormatter( this ChainBuilder builder )
        {
            builder.AddChainLink< ArgumentFormatter >();
            return builder;
        }

        public static ChainBuilder AddDownloader(this ChainBuilder builder)
        {
            builder.AddChainLink< Downloader >();
            return builder;
        }

        public static ChainBuilder AddUnzipper(this ChainBuilder builder)
        {
            builder.AddChainLink< Unzipper >();
            return builder;
        }

        public static ChainBuilder AddExtensionChecker(this ChainBuilder builder)
        {
            builder.AddChainLink< ExtensionChecker >();
            return builder;
        }

        public static ChainBuilder AddCsvHeaderChecker(this ChainBuilder builder)
        {
            builder.AddChainLink< CsvHeaderChecker >();
            return builder;
        }

        public static ChainBuilder AddMapper(this ChainBuilder builder)
        {
            builder.AddChainLink< Mapper >();
            return builder;
        }

        public static ChainBuilder AddUpdater(this ChainBuilder builder)
        {
            builder.AddChainLink< Updater >();
            return builder;
        }

        public static ChainBuilder AddTempFileCreator(this ChainBuilder builder)
        {
            builder.AddChainLink< TempFileCreator >();
            return builder;
        }
    }
}
