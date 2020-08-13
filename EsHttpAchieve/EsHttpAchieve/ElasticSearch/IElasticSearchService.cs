using EsHttpAchieve.IConstraint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsHttpAchieve.ElasticSearch.Params;
using static EsHttpAchieve.ElasticSearch.ElasticSearchEnums.ElasticSearchEnum;
using EsHttpAchieve.ElasticSearch.Tools.QueryGenerates;

namespace EsHttpAchieve.ElasticSearch
{
    public interface IElasticSearchService
    {
        Task<EsHttpResult> IndexAsync<T>(T data) where T : IHasGuidAsId;

        Task<EsHttpResult> CreateAsync<T>(T data) where T : IHasGuidAsId;

        Task<EsHttpResult> DeleteAsync<T>(Guid id) where T : IHasGuidAsId;

        Task<EsHttpResult> UpdateAsync<T>(T data) where T : IHasGuidAsId;

        Task<EsHttpResult> SearchAsync<T>(QueryNode queryNode) where T : IHasGuidAsId;
    }
}
