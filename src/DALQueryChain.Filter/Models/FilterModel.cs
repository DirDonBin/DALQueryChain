using DALQueryChain.Filter.Enums;

namespace DALQueryChain.Filter.Models
{
    internal class FilterModel
    {
        public string SrcFieldName { get; set; } = null!;
        public string DestFieldName { get; set; } = null!;
        public QSFilterCondition[] ConditionTypes { get; set; } = null!;
    }
}
