using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Filter.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class QCFilterAttribute : Attribute
    {
        public string Name { get; set; } = "default";

        public QCFilterAttribute()
        {

        }
    }
}
