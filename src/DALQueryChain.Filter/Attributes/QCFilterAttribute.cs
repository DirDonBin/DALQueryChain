using DALQueryChain.Filter.Enums;

namespace DALQueryChain.Filter.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class QCFilterAttribute : Attribute
    {
        public string Name { get; set; } = "default";
        public string? FieldName { get; set; }

        public bool NullValueIgnore { get; set; } = false;
        public bool StringSensitiveCase { get; set; } = true;

        public QCFilterCondition[] ConditionTypes { get; init; }

        public QCFilterAttribute(params QCFilterCondition[] conditionTypes)
        {
            ConditionTypes = conditionTypes;
        }

        public QCFilterAttribute(string name, params QCFilterCondition[] conditionTypes)
        {
            ConditionTypes = conditionTypes;
            Name = name;
        }

        public QCFilterAttribute(object name, params QCFilterCondition[] conditionTypes)
        {
            ConditionTypes = conditionTypes;
            Name = name.ToString()!;
        }
    }
}
