using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridAi.TestTask.ConsoleDbUpdater.Extensions;
using HybridAi.TestTask.ConsoleDbUpdater.Models;
using HybridAi.TestTask.Data;
using HybridAi.TestTask.Data.Comparators;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.Data.Services.UpdaterService;

namespace HybridAi.TestTask.ConsoleDbUpdater.ChainLinks
{
    public abstract class Updater : ChainLink, IDisposable
    {
        #region fields

        private IpDbContext? _dbContext;
        protected readonly (int, int) _Negative = (-1, -1);

        #endregion


        #region ctor

        protected Updater( IChainLink< Request, IResponse< Request > > successor ) : base( successor )
        { }


        #endregion


        #region properties

        protected IpDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    var options = DbContextOptionsFactory.Instance.DbContextOptions;
                    _dbContext = new IpDbContext(options);
                }

                return _dbContext;
            }
        }

        #endregion


        #region methods

        public override IResponse<Request> Process(Request request)
        {
            LoggerFactory.Instance.Log( "Start update database..." );

            if (request is ImportedModelsRequest modelsRequest)
            {
                if ( modelsRequest.ModelCollections.Any() != true
                    || modelsRequest.ModelCollections.All(c => !c.Any()))
                {
                    return new FailRequest("Imported model collections are empty.").Response;
                }

                return _Process( modelsRequest.ModelCollections );
            }

            Dispose();
            return base.Process(request);
        }


        protected abstract IResponse<Request> _Process( List<IEntity>[] importedEntities );

        protected IResponse<Request> _GetDoneRequest( int newCount, int updCount)
        {
            Dispose();
            return new DoneRequest( newCount, updCount, $"There are {newCount} new records and {updCount} updated records." ).Response;
        }

        protected IResponse<Request> _GetFailRequest()
        {
            Dispose();
            return new FailRequest($"Fail.").Response;
        }



        #endregion


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose( bool disposing )
        {
            if (!disposedValue) {
                if (disposing) {
                    _dbContext?.Dispose();
                    _dbContext = null;
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose( true );
        }
        #endregion
    }
}
