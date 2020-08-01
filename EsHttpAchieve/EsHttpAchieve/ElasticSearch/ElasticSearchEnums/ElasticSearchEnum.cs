using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsHttpAchieve.ElasticSearch.ElasticSearchEnums
{
    public class ElasticSearchEnum
    {
        public enum ElasticOperation
        {
            _doc = 1,
            _create = 2,
            _update = 3,
            _search = 4
        }

    }
}
