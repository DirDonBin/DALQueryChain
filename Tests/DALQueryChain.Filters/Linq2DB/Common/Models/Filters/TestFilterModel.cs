using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Enums;
using ManualTest.Linq2Db.Context;

namespace DALQueryChain.Tests.Linq2DB.Common.Models.Filters
{
    public record TestFilterModel
    {
        [QCFilter(TestFiltersEnum.IdEquals, QSFilterCondition.Equals)]
        [QCFilter(TestFiltersEnum.IdGreater, QSFilterCondition.Greater)]
        public int? Id { get; set; }

        [QCFilter(TestFiltersEnum.ContainsName, QSFilterCondition.Contains)]
        public string? Name { get; set; }

        [QCFilter(TestFiltersEnum.BetweenDate, QSFilterCondition.LessOrEqual, FieldName = nameof(Product.Created))]
        public DateTime? DateFrom { get; set; }

        [QCFilter(TestFiltersEnum.BetweenDate, QSFilterCondition.Greater, FieldName = nameof(Product.Created))]
        public DateTime? DateTo { get; set; }

        [QCFilter(TestFiltersEnum.Category, QSFilterCondition.Equals)]
        public int? CategoryId { get; set; }

        [QCFilter(TestFiltersEnum.Categories, QSFilterCondition.Contains, FieldName = nameof(Product.CategoryId))]
        public List<int>? Categories { get; set; }

        [QCFilter(TestFiltersEnum.Ids, QSFilterCondition.Contains, FieldName = nameof(Product.Id))]
        public int[]? Ids { get; set; }

        [QCFilter(TestFiltersEnum.Count, QSFilterCondition.GreaterOrEqual)]
        public int? Count { get; set; }

        [QCFilter(TestFiltersEnum.Raiting1, QSFilterCondition.Greater, FieldName = nameof(Product.Raiting))]
        public int? Raiting1 { get; set; }

        [QCFilter(TestFiltersEnum.Raiting2, QSFilterCondition.LessOrEqual, FieldName = nameof(Product.Raiting))]
        public float? Raiting2 { get; set; }

        [QCFilter(TestFiltersEnum.PriceGreater, QSFilterCondition.Greater)]
        [QCFilter(TestFiltersEnum.PriceLess, QSFilterCondition.Less, FieldName = nameof(Product.Price))]
        public decimal? Price { get; set; }
    }
}
