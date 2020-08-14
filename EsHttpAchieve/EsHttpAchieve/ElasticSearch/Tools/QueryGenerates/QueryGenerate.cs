using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsHttpAchieve.Enums;
using EsHttpAchieve.Extensions;
using static EsHttpAchieve.Enums.EsQueryEnum;

namespace EsHttpAchieve.ElasticSearch.Tools.QueryGenerates
{
    public static class QueryGenerate
    {
        public static QueryNode AddChildNode(this QueryNode fatherNode, string name, string value = null)
        {
            var sonNode = new QueryNode(name, value) { FatherNode = fatherNode };

            fatherNode.Node.Add(sonNode);
            return sonNode;
        }

        public static QueryNode AddNode(this QueryNode fatherNode, string name, string value = null)
        {
            var sonNode = new QueryNode(name, value) {FatherNode = fatherNode};
            fatherNode.Node.Add(sonNode);
            return fatherNode;
        }

        public static QueryNode AddArrayNode(this QueryNode fatherNode, string name, string value = null)
        {
            var sonNode = new QueryNode(name, value, NodeType.数组节点) { FatherNode = fatherNode };
            fatherNode.Node.Add(sonNode);
            return sonNode;
        }


        public static string GenerateQueryString(this QueryNode queryNode)
        {
            // 为叶子节点，则返回节点值
            if (queryNode.NodeType == NodeType.叶子节点)
            {
                if (queryNode.FatherNode.NodeType == NodeType.数组节点)
                {
                    return $"\"{queryNode.Name}\"";
                }

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

                if (queryNode.NodeType == NodeType.数组节点)
                {
                    return  $"\"{queryNode.Name}\"" + ":[" + sonStr + "]";
                }

                return queryNode.Name == null ? "{" + sonStr + "}" : $"\"{queryNode.Name}\"" + ":{" + sonStr + "}";
            }
            else
            {
                return $"\"{queryNode.Name}\"" + ":{}";
            }
        }
    }
}
