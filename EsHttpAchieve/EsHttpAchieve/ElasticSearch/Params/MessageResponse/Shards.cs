using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsHttpAchieve.ElasticSearch.Params.MessageResponse
{
    public class Shards
    {
        public int total { get; set; }

        public int successful { get; set; }

        public int skipped { get; set; }

        public int failed { get; set; }
    }
}
