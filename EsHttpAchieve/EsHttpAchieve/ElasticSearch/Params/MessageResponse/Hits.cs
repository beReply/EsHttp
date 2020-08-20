using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsHttpAchieve.ElasticSearch.Params.MessageResponse
{
    public class Hits<T>
    {
        public Total total { get; set; }

        public string max_score { get; set; }

        public List<Content<T>> hits { get; set; }
    }
}
