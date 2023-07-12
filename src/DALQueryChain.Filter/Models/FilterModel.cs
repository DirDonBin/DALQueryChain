using DALQueryChain.Filter.Enums;

namespace DALQueryChain.Filter.Models
{
    internal record FilterModel
    {
        public string SrcFieldName { get; set; } = null!;
        public string DestFieldName { get; set; } = null!;

        public QCFilterSetting Settings { get; set; } = new();

        public QCFilterCondition[] ConditionTypes { get; set; } = null!;
    }
}
