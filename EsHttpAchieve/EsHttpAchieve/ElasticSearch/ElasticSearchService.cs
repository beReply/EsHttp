using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EsHttpAchieve.IConstraint;
using Newtonsoft.Json;

namespace EsHttpAchieve.ElasticSearch
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IEsHttpHelper _esHttpHelper;

        public ElasticSearchService(IEsHttpHelper esHttpHelper)
        {
            _esHttpHelper = esHttpHelper;
        }

        public async Task IndexAsync<T>(T data) where T : IHasGuidAsId
        {
            var body = JsonConvert.SerializeObject(data);
            Console.WriteLine(body);
            var res = await _esHttpHelper
                .SendAsync(HttpMethod.Put, typeof(T).Name.ToLower(), "_doc", data.Id.ToString(), body);
            Console.WriteLine(res.Message);
        }

    }
}
