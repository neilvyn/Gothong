using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GothongApp.Services.Rest
{
    public interface IRestService
    {
        Task Request(EnumHttpMethod method, string url, CancellationToken ctoken, string ws_query = "", object dictionary = null, string authHeader = null, int timeout = 100);
    }
}
