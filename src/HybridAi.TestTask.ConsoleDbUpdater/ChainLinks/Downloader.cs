using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Models;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public class Downloader : ChainLink
    {
        private readonly string _downloadFolder = Environment.GetFolderPath( Environment.SpecialFolder.InternetCache );

        public Downloader( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
        { }

        public override IResponse< Request > Process( Request request )
        {
            LoggerFactory.Instance.Log( "Start download process..." );

            if (request is UrlRequest urlRequest)
            {
                var url = urlRequest.Url;


                if (!String.IsNullOrWhiteSpace( url ) && _Download( urlRequest.Url, out var path ) )
                {
                    #nullable disable
                    return base.Process( new FileLocationRequest( path ) );
                    #nullable restore
                }

                return base.Process( request );
            }

            return base.Process( request );
        }

        private bool _Download( string url, out string? fileName )
        {
            Task< string? > task = Task.Run< string? >( async () =>
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

                HttpResponseMessage? response = null;

                try {
                    response = await client.GetAsync( url );
                }
                catch ( Exception ex ) {
                    LoggerFactory.Instance.Log( ex.Message );
                    return null;
                }

                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    var fn = content.Headers.ContentDisposition.FileName;

                    await using var contentStream = await content.ReadAsStreamAsync();

                    var fullPath = Path.Combine( _downloadFolder, fn );
                    if ( !File.Exists( fullPath ) )
                    {
                        await using var fs = File.Create( fullPath );
                        await contentStream.CopyToAsync( fs );
                    }

                    return fullPath;
                }

                return null;
            } );

            fileName = task.Result;

            return !String.IsNullOrWhiteSpace( fileName );
        }

    }
}
