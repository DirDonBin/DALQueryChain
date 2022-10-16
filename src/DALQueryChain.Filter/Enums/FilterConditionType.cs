using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Filter.Enums
{
    public enum QSFilterConditionType
    {
        Equals,
        NotEqual,
        Less,
        LessOrEqual,
        Greater,
        GreaterOrEqual,
        Between,
        Contains
    }
}
