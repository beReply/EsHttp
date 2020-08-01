using EsHttpAchieve.ElasticSearch.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EsHttpAchieve.ElasticSearch
{
    public interface IEsHttpHelper
    {
        Task<EsHttpResult> SendAsync(HttpMethod httpMethod, string table, string operation, string id, string body);
    }
}
