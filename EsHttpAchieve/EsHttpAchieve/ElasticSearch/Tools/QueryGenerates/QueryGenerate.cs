using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsHttpAchieve.Extensions;

namespace EsHttpAchieve.ElasticSearch.Tools.QueryGenerates
{
    public static class QueryGenerate
    {
        public static QueryNode AddChildNode(this QueryNode fatherNode, string name, string value)
        {
            var sonNode = new QueryNode {Name = name, Value = value, FatherNode = fatherNode};
            fatherNode.Node.Add(sonNode);
            return sonNode;
        }

        public static QueryNode AddNode(this QueryNode fatherNode, string name, string value)
        {
            var sonNode = new QueryNode { Name = name, Value = value, FatherNode = fatherNode };
            fatherNode.Node.Add(sonNode);
            return fatherNode;
        }


        public static string GenerateQueryString(this QueryNode queryNode)
        {
            if (!queryNode.Value.IsNullOrWhiteSpace())
            {
                return $"\"{queryNode.Name}\":\"{queryNode.Value}\"";
            }

            if (queryNode.Node.Count != 0)
            {
                string sonStr = "";
                foreach (var node in queryNode.Node)
                {
                    sonStr += node.GenerateQueryString() + ",";
                }
                sonStr = sonStr.Substring(0, sonStr.Length - 1);
                return queryNode.Name == null ? "{" + sonStr + "}" : $"\"{queryNode.Name}\"" + ":{" + sonStr + "}";
            }
            else
            {
                return $"\"{queryNode.Name}\"" + ":{}";
            }
        }
    }
}
