using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Filter.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class QCSortingAttribute : Attribute
    {
        public string? FieldName { get; set; }

        public QCSortingAttribute()
        {
        }
    }
}
