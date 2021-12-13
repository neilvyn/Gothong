using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GothongApp.Services.Predefined;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace GothongApp.Services.Rest
{
    public class RestService : IRestService
    {
        WeakReference<IResponseConnector> restResponseDelegate;
        public IResponseConnector RestResponseDelegate
        {
            get
            {
                IResponseConnector _restResponseDelegate;
                return restResponseDelegate.TryGetTarget(out _restResponseDelegate) ? _restResponseDelegate : null;
            }
            set
            {
                restResponseDelegate = new WeakReference<IResponseConnector>(value);
            }
        }

        async public Task Request(EnumHttpMethod method, string url, CancellationToken ctoken, string ws_query = "", object dictionary = null, string authHeader = null, int timeout = 100)
        {
            LogConsole.AsyncOutput(this, "[" + method + "] " + url);

            var handler = new TimeoutHandler
            {
                DefaultTimeout = TimeSpan.FromSeconds(100),
                InnerHandler = new HttpClientHandler()
            };

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.MaxResponseContentBufferSize = 256000;

                if (!string.IsNullOrEmpty(authHeader))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", authHeader);
                }

                client.Timeout = Timeout.InfiniteTimeSpan;
                HttpRequestMessage request;

                switch (method)
                {
                    case EnumHttpMethod.Get:
                        request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
                        request.SetTimeout(TimeSpan.FromSeconds(timeout));
                        using (var response = await client.SendAsync(request, ctoken))
                        {
                            await RequestAsync(response, ctoken, ws_query);
                        }
                        break;
                    case EnumHttpMethod.Post:
                        LogConsole.AsyncOutput(this, "=========================\n" + JToken.Parse(dictionary.ToString()));
                        request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                        request.SetTimeout(TimeSpan.FromSeconds(timeout));
                        request.Content = new StringContent((string)dictionary, Encoding.UTF8, "application/json");
                        using (var response = await client.SendAsync(request, ctoken))
                        {
                            await RequestAsync(response, ctoken, ws_query);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        async Task RequestAsync(HttpResponseMessage response, CancellationToken ct, string ws_query)
        {
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
                RestResponseDelegate?.ReceiveJSONData(result, ws_query);
            else
                RestResponseDelegate?.ReceiveError(Constants.CriticalTitleAlert, "Something went wrong.", ws_query);
        }
    }
}
