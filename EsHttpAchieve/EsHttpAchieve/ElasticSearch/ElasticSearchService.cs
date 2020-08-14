using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EsHttpAchieve.ElasticSearch.ElasticSearchEnums;
using EsHttpAchieve.ElasticSearch.Params;
using EsHttpAchieve.ElasticSearch.Tools.QueryGenerates;
using EsHttpAchieve.IConstraint;
using Newtonsoft.Json;
using static EsHttpAchieve.ElasticSearch.ElasticSearchEnums.ElasticSearchEnum;

namespace EsHttpAchieve.ElasticSearch
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IEsHttpHelper _esHttpHelper;

        public ElasticSearchService(IEsHttpHelper esHttpHelper)
        {
            _esHttpHelper = esHttpHelper;
        }

        #region CURD

        /// <summary>
        /// 索引一个文档
        ///     如果ID不存在，创建新的文档。否则先删除现在的文档，再创建新的文档，版本会增加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<EsHttpResult> IndexAsync<T>(T data) where T : IHasGuidAsId
        {
            return await _esHttpHelper
                .SendAsync(HttpMethod.Put, typeof(T).Name.ToLower(),
                    ElasticOperation._doc.ToString(), data.Id.ToString(), JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// 索引一个文档
        ///     如果ID已经存在，会失败
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<EsHttpResult> CreateAsync<T>(T data) where T : IHasGuidAsId
        {
            return await _esHttpHelper
                .SendAsync(HttpMethod.Put, typeof(T).Name.ToLower(),
                    ElasticOperation._create.ToString(), data.Id.ToString(), JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// 删除一个文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EsHttpResult> DeleteAsync<T>(Guid id) where T : IHasGuidAsId
        {
            return await _esHttpHelper
                .SendAsync(HttpMethod.Delete, typeof(T).Name.ToLower(),
                    ElasticOperation._doc.ToString(), id.ToString(), null);
        }

        /// <summary>
        /// 更新一个文档
        ///     文档必须已经存在，更新会对相应字段做增量修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<EsHttpResult> UpdateAsync<T>(T data) where T : IHasGuidAsId
        {
            return await _esHttpHelper
                .SendAsync(HttpMethod.Post, typeof(T).Name.ToLower(),
                    ElasticOperation._update.ToString(), data.Id.ToString(), JsonConvert.SerializeObject(new { doc = data }));
        }

        #endregion

        public async Task<EsHttpResult> SearchAsync<T>(QueryNode queryNode) where T : IHasGuidAsId
        {
            var body = queryNode.GenerateQueryString();

            return await _esHttpHelper
                .SendAsync(HttpMethod.Get, typeof(T).Name.ToLower(),
                    ElasticOperation._search.ToString(), null, body);
        }
    }
}
