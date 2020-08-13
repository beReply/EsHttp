using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsHttpAchieve.ElasticSearch.Tools.QueryGenerates
{
    public class QueryNode
    {
        public QueryNode FatherNode { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }

        public List<QueryNode> Node = new List<QueryNode>();
    }
}
