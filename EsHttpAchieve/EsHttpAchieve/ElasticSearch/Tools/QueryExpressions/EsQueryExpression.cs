using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EsHttpAchieve.ElasticSearch.Tools.QueryGenerates;
using EsHttpAchieve.Extensions;

namespace EsHttpAchieve.ElasticSearch.Tools.QueryExpressions
{
    public static class EsQueryExpression
    {

        public static QueryNode Query(this QueryNode node)
        {
            return node.AddNodeAndToChild("query");
        }

        public static QueryNode Range<T>(this QueryNode node, Expression<Func<T, bool>> expression)
        {
            var visitor = new EsExpressionVisitor();
            visitor.Visit(expression);

            var exprString = visitor.GetWhere();
            var exprStr = exprString.Split(" ");

            node.AddNodeAndToChild("range")
                .AddNodeAndToChild(exprStr[0])
                .AddNode(exprStr[1].ToEsOperator(), exprStr[2]);

            return node;
        }
    }
}
