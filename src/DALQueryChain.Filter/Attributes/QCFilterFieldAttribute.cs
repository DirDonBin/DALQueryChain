﻿using DALQueryChain.Filter.Enums;

namespace DALQueryChain.Filter.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class QCFilterFieldAttribute : Attribute
    {
        public string Name { get; set; } = "default";
        public string? FieldName { get; set; }
        public QSFilterConditionType[] ConditionTypes { get; init; }

        public QCFilterFieldAttribute(params QSFilterConditionType[] conditionTypes)
        {
            ConditionTypes = conditionTypes;
        }
    }
}
