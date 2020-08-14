using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsHttpAchieve.ElasticSearch;
using EsHttpAchieve.ElasticSearch.ElasticSearchEnums;
using EsHttpAchieve.Entities;
using EsHttpAchieve.Enums;
using EsHttpAchieve.Extensions;
using static EsHttpAchieve.Enums.ProductEnum;
using System.Security.Cryptography;
using EsHttpAchieve.ElasticSearch.Params;
using EsHttpAchieve.ElasticSearch.Tools.QueryGenerates;

namespace EsHttpAchieve.Controllers
{
    public class TextController : Controller
    {
        private readonly IElasticSearchService _elasticSearchService;

        public TextController(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        [HttpPost("/Text/Index")]
        public async Task IndexAsync()
        {
            var test = new TestEntity
            {
                Id = Guid.NewGuid(),
                Message = RandomGeneratorExtension.GenerateChineseWord(30),
                CreateTime = DateTime.Now,
                Remark = RandomGeneratorExtension.GenerateChineseWord(10)
            };

            await _elasticSearchService.IndexAsync(test);
        }

        [HttpPost("/Text/Product")]
        public async Task ProductionAsync()
        {
            var companyArray = Enum.GetValues(typeof(Company)) as Company[];
            var productTypeArray = Enum.GetValues(typeof(ProductType)) as ProductType[];
            for (int i = 0; i < 10000; i++)
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Company = RandomGeneratorExtension.RandomArray(companyArray).ToString(),
                    ProductType = RandomGeneratorExtension.RandomArray(productTypeArray).ToString(),
                    Description = RandomGeneratorExtension.GenerateChineseWord(50),
                    Remark = RandomGeneratorExtension.GenerateChineseWord(10),
                    Price = Convert.ToDecimal(RandomNumberGenerator.Create().GeneratorDigitalRandom(3)),
                    CreateTime = DateTime.Now
                };
                await _elasticSearchService.IndexAsync(product);
            }
        }

        [HttpPost("/Search/Product")]
        public async Task<EsHttpResult> SearchProductAsync(string searchStr1, string searchStr2)
        {
            var queryNode = new QueryNode();

            queryNode.AddNodeAndToChild("query")
                .AddNodeAndToChild("query_string")
                .AddNode("query", $"{searchStr1} AND {searchStr2}")
                .AddArrayNodeAndToChild("fields").AddNode(null, "Description").AddNode( null, "Remark");




            var res = await  _elasticSearchService.SearchAsync<Product>(queryNode);

           Console.WriteLine(res);

           return res;
        }
    }
}
