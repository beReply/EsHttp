using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EsHttpAchieve.ElasticSearch.Params;
using EsHttpAchieve.Extensions;

namespace EsHttpAchieve.ElasticSearch
{
    public class EsHttpHelper : IEsHttpHelper
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _url = "http://localhost:9200";

        public EsHttpHelper(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<EsHttpResult> SendAsync(HttpMethod httpMethod, string table, string operation, string id, string body)
        {
            if (table.IsNullOrWhiteSpace() || operation.IsNullOrWhiteSpace())
            {
                Console.WriteLine("参数错误");
                return new EsHttpResult { IsSuccess = false, Message = "参数错误" };
            }

            var path =  _url + $"/{table}/{operation}/{id}".Trim('/').Replace("//","/");

            EsHttpResult esHttpResult;
            try
            {
                var client = _clientFactory.CreateClient();
                var content = new StringContent(body, Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var request = new HttpRequestMessage(httpMethod, path) {Content = content};

                var response = await client.SendAsync(request);


                esHttpResult = new EsHttpResult{IsSuccess = true, Message = await response.Content.ReadAsStringAsync()};
            }
            catch (Exception e)
            {
                Console.WriteLine("请求失败");
                Console.WriteLine(e);
                return new EsHttpResult { IsSuccess = false, Message = "请求失败" };
            }

            return esHttpResult;
        }
    }
}
