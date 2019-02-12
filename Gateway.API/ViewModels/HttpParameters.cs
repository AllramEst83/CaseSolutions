using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Gateway.API.ViewModels
{
    public class HttpParameters
    {
        public string RequestUrl { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public Guid Id { get; set; }
        public object Content { get; set; }
        public HttpMethod HttpVerb { get; set; }
    }
}
