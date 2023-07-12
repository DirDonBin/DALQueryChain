using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Enums;
using DALQueryChain.Filter.Models;
using ManualTest.Linq2Db.Context;

namespace DALQueryChain.Tests.Linq2DB.Common.Models.Filters
{
    public record TestFilterModel
    {
        [QCFilter(TestFiltersEnum.IdEquals, QCFilterCondition.Equals)]
        [QCFilter(TestFiltersEnum.IdGreater, QCFilterCondition.Greater)]
        public int? Id { get; set; }

        [QCFilter(TestFiltersEnum.ContainsName, QCFilterCondition.Contains)]
        public string? Name { get; set; }

        [QCFilter(TestFiltersEnum.BetweenDate, QCFilterCondition.LessOrEqual, FieldName = nameof(Product.Created))]
        public DateTime? DateFrom { get; set; }

        [QCFilter(TestFiltersEnum.BetweenDate, QCFilterCondition.Greater, FieldName = nameof(Product.Created))]
        public DateTime? DateTo { get; set; }

        [QCFilter(TestFiltersEnum.Category, QCFilterCondition.Equals, FieldName = "Category.Id", NullValueIgnore = true)]
        [QCSorting(FieldName = "Category.Id")]
        public int? CategoryId { get; set; }

        [QCFilter(TestFiltersEnum.Categories, QCFilterCondition.Contains, FieldName = "Category.Id")]
        public List<int>? Categories { get; set; }

        [QCFilter(TestFiltersEnum.AnyIds, QCFilterCondition.Contains, FieldName = "Products.Id")]
        [QCFilter(TestFiltersEnum.Ids, QCFilterCondition.Contains, FieldName = nameof(Product.Id))]
        public int[]? Ids { get; set; }

        [QCFilter(TestFiltersEnum.Count, QCFilterCondition.GreaterOrEqual)]
        [QCSorting]
        public int? Count { get; set; }

        [QCFilter(TestFiltersEnum.Raiting1, QCFilterCondition.Greater, FieldName = nameof(Product.Raiting))]
        public int? Raiting1 { get; set; }

        [QCFilter(TestFiltersEnum.Raiting2, QCFilterCondition.LessOrEqual, FieldName = nameof(Product.Raiting))]
        public float? Raiting2 { get; set; }

        [QCFilter(TestFiltersEnum.PriceGreater, QCFilterCondition.Greater)]
        [QCFilter(TestFiltersEnum.PriceLess, QCFilterCondition.Less, FieldName = nameof(Product.Price))]
        [QCSorting]
        public decimal? Price { get; set; }


        public QCSorting? Sorting { get; set; }
        public List<QCSorting>? SortingList { get; set; }
        public QCPaginate? Paginate { get; set; }
    }
}
