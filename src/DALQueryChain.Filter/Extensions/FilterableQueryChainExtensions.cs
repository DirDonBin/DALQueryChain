using DALQueryChain.Filter.Models;
using DALQueryChain.Interfaces.QueryBuilder.Get;

namespace DALQueryChain.Filter.Extensions
{
    public static class FilterableQueryChainExtensions
    {

        public static IFilterableQueryChain<TEntity> Filter<TEntity, TFilterModel>(this IFilterableQueryChain<TEntity> query, TFilterModel model, string name = "default")
            where TFilterModel : class
        {
            return FilterGenerator.FilterGenerate(query, model, name);
        }

        public static IFilterableQueryChain<TEntity> Filter<TEntity, TFilterModel>(this IFilterableQueryChain<TEntity> query, TFilterModel model, object name)
            where TFilterModel : class
        {
            return FilterGenerator.FilterGenerate(query, model, name.ToString()!);
        }

        public static IOrderableQueryChain<TEntity> Sorting<TEntity, TSortingModel>(this IFilterableQueryChain<TEntity> query, Type modelType, TSortingModel? sorting)
            where TSortingModel : QCSorting
        {
            var sortList = new List<TSortingModel?> { sorting };
            return FilterGenerator.SortingGenerate(query, modelType, sortList);
        }

        public static IOrderableQueryChain<TEntity> Sorting<TEntity, TSortingModel>(this IFilterableQueryChain<TEntity> query, Type modelType, IEnumerable<TSortingModel?>? sorting)
            where TSortingModel : QCSorting
        {
            return FilterGenerator.SortingGenerate(query, modelType, sorting);
        }

        public static IFilterableQueryChain<TEntity> Paginate<TEntity, TPaginateModel>(this IFilterableQueryChain<TEntity> query, TPaginateModel? model)
            where TPaginateModel : QCPaginate
        {
            return FilterGenerator.PaginateGenerate(query, model);
        }
    }
}
