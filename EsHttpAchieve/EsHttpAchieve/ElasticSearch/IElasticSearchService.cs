using EsHttpAchieve.IConstraint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsHttpAchieve.ElasticSearch
{
    public interface IElasticSearchService
    {
        Task IndexAsync<T>(T data) where T : IHasGuidAsId;
    }
}
