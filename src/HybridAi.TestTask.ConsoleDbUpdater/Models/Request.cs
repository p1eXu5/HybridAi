using System;
using System.Collections.Generic;
using System.Text;

namespace HybridAi.TestTask.ConsoleDbUpdater.Models
{
    public class Request
    {
        public static Request EmptyRequest => new Request();

        public virtual IResponse< Request > Response => new Response< Request >( EmptyRequest );

    }
}
