using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class Unzipper : ChainLink
    {
        public const string ZIP_EXT = ".zip";
        public readonly string[] MaintainedExtensions = new[] { ".txt", ".csv" };

        public Unzipper( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
        { }

        public override IResponse< Request > Process( Request request )
        {
            LoggerFactory.Instance.Log( "Start unzip process..." );

            if ( request is FileLocationRequest fileLocationRequest ) 
            {
                string path = fileLocationRequest.Path;
                if ( !Path.GetExtension( path ).Equals( ZIP_EXT, StringComparison.InvariantCultureIgnoreCase ) ) {
                    return base.Process( request );
                }

                try {
                    using ZipArchive zipArchive = ZipFile.OpenRead( fileLocationRequest.Path );
                    
                    if ( zipArchive.Entries.Count == 0 ) {
                        return base.Process( request );
                    }

                    // check directory
                    string destinationPath = Path.Combine( Path.GetTempPath(), Path.GetFileNameWithoutExtension( path ) );
                    destinationPath += Path.DirectorySeparatorChar;
                        
                    if ( !Directory.Exists( destinationPath ) ) {
                        Directory.CreateDirectory( destinationPath );
                    }

                    List< string > targets = new List< string >( zipArchive.Entries.Count );

                    foreach ( ZipArchiveEntry entry in zipArchive.Entries ) 
                    {
                        if ( MaintainedExtensions.Contains( Path.GetExtension( entry.FullName ) ) ) {
                            var extractPath = Path.Combine( destinationPath, Path.GetFileNameWithoutExtension( entry.FullName ) );

                            try {
                                entry.ExtractToFile( extractPath, true );
                            }
                            catch ( Exception ex ) {
                                LoggerFactory.Instance.Log( ex.Message );
                            }

                            targets.Add( extractPath );
                        }
                    }

                    if ( targets.Count > 0 ) {
                        return base.Process( new FolderRequest( targets ) );
                    }
                }
                catch ( Exception ex ) {
                    LoggerFactory.Instance.Log( ex.Message );
                }
            }

            return base.Process( request );
        }

    }
}
