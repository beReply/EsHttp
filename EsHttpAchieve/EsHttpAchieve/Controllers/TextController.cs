using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsHttpAchieve.ElasticSearch;
using EsHttpAchieve.Entities;
using EsHttpAchieve.Extensions;

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
    }
}
