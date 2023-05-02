using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Enums;
using ManualTest.Linq2Db.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Core.Models.Filters
{
    public class TestFilterModel
    {
        [QCFilterField(TestFiltersEnum.IdEquals, QSFilterConditionType.Equals)]
        [QCFilterField(TestFiltersEnum.IdGreater, QSFilterConditionType.Greater)]
        public int? Id { get; set; }

        [QCFilterField(TestFiltersEnum.ContainsName, QSFilterConditionType.Contains)]
        public string? Name { get; set; }

        [QCFilterField(TestFiltersEnum.BetweenDate, QSFilterConditionType.GreaterOrEqual, FieldName = nameof(Product.Created))]
        public DateTime? DateFrom { get; set; }

        [QCFilterField(TestFiltersEnum.BetweenDate, QSFilterConditionType.Less, FieldName = nameof(Product.Created))]
        public DateTime? DateTo { get; set; }

        [QCFilterField(TestFiltersEnum.Category, QSFilterConditionType.Equals)]
        public int? CategoryId { get; set; }

        [QCFilterField(TestFiltersEnum.Categories, QSFilterConditionType.Contains, FieldName = nameof(Product.CategoryId))]
        public List<int>? Categories { get; set; }

        [QCFilterField(TestFiltersEnum.Ids, QSFilterConditionType.Contains, FieldName = nameof(Product.Id))]
        public int[]? Ids { get; set; }

        [QCFilterField(TestFiltersEnum.Count, QSFilterConditionType.GreaterOrEqual)]
        public int? Count { get; set; }

        [QCFilterField(TestFiltersEnum.Raiting1, QSFilterConditionType.Greater, FieldName = nameof(Product.Raiting))]
        public int? Raiting1 { get; set; }

        [QCFilterField(TestFiltersEnum.Raiting2, QSFilterConditionType.LessOrEqual, FieldName = nameof(Product.Raiting))]
        public float? Raiting2 { get; set; }

        [QCFilterField(TestFiltersEnum.PriceGreater, QSFilterConditionType.Greater)]
        [QCFilterField(TestFiltersEnum.PriceLess, QSFilterConditionType.Less)]
        public decimal? Price { get; set; }
    }
}
