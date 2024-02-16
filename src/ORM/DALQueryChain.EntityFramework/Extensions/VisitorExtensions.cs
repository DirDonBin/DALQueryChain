using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.EntityFramework.Extensions
{
    internal static class VisitorExtensions
    {
        /// <summary>
        /// Visits a collection of <paramref name="expressions"/> and returns new collection if it least one expression has been changed.
        /// Otherwise the provided <paramref name="expressions"/> are returned if there are no changes.
        /// </summary>
        /// <param name="visitor">Visitor to use.</param>
        /// <param name="expressions">Expressions to visit.</param>
        /// <returns>
        /// New collection with visited expressions if at least one visited expression has been changed; otherwise the provided <paramref name="expressions"/>.
        /// </returns>
        public static IReadOnlyList<T> VisitExpressions<T>(this ExpressionVisitor visitor, IReadOnlyList<T> expressions)
           where T : Expression
        {
            T[]? visitedExpression = null;

            for (var i = 0; i < expressions.Count; i++)
            {
                var expression = expressions[i];
                var visited = (T)visitor.Visit(expression);

                if (visited != expression && visitedExpression is null)
                {
                    visitedExpression = new T[expressions.Count];

                    for (var j = 0; j < i; j++)
                    {
                        visitedExpression[j] = expressions[j];
                    }
                }

                if (visitedExpression is not null)
                    visitedExpression[i] = visited;
            }

            return visitedExpression ?? expressions;
        }
    }
}
