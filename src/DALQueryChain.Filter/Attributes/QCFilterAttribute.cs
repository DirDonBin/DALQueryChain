using DALQueryChain.Filter.Enums;

namespace DALQueryChain.Filter.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class QCFilterAttribute : Attribute
    {
        public string Name { get; set; } = "default";
        public string? FieldName { get; set; }
        public QSFilterCondition[] ConditionTypes { get; init; }

        public QCFilterAttribute(params QSFilterCondition[] conditionTypes)
        {
            ConditionTypes = conditionTypes;
        }

        public QCFilterAttribute(string name, params QSFilterCondition[] conditionTypes)
        {
            ConditionTypes = conditionTypes;
            Name = name;
        }

        public QCFilterAttribute(object name, params QSFilterCondition[] conditionTypes)
        {
            ConditionTypes = conditionTypes;
            Name = name.ToString()!;
        }
    }
}
