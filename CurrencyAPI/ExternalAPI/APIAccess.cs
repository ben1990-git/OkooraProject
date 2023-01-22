using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalAPI
{
    public abstract class APIAccess
    {
        abstract protected void AuthenticatRequest(HttpRequestMessage httpClient);
    }
}
