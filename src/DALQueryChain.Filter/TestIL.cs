using DALQueryChain.Filter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Filter
{
    internal class TestIL
    {
        internal Func<TestModel, bool> Where(TestModel filter)
        {
            Func<TestModel, bool> tmp = x => (x.Id == filter.Id && x.DateCreate <= filter.DateCreate) || (x.Id == filter.Id && x.Name.Contains(filter.Name));
            return tmp;
        }
    }
}
