using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public interface IResponse< out T >
    {
        T Request { get; }
    }

    public class Response<T> : Request, IResponse< T >
        where T : Request
    {
        public Response( T request )
        {
            Request = request;
        }

        public T Request { get; }
    }
}
