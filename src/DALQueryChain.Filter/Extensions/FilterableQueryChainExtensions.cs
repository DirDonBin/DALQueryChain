using DALQueryChain.Interfaces.QueryBuilder.Get;

namespace DALQueryChain.Filter.Extensions
{
    public static class FilterableQueryChainExtensions
    {

        public static IFilterableQueryChain<TEntity> Filter<TEntity, TFilterModel>(this IFilterableQueryChain<TEntity> query, TFilterModel model, string name = "default")
        {
            return FilterGenerator.Generate(query, model, name);
        }

        public static IFilterableQueryChain<TEntity> Filter<TEntity, TFilterModel>(this IFilterableQueryChain<TEntity> query, TFilterModel model, object name)
        {
            return FilterGenerator.Generate(query, model, name.ToString()!);
        }
    }
}
