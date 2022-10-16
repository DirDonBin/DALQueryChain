using DALQueryChain.Filter.Attributes;
using DALQueryChain.Filter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Filter.Models
{
    [QCFilter]
    [QCFilter(Name = "Test 1")]
    [QCFilter(Name = "Test 2")]
    [QCFilter(Name = "Test 3")]
    public class TestModel
    {
        [QCFilterField(QSFilterConditionType.Equals)]
        [QCFilterField(QSFilterConditionType.Greater, Name = "Test 1")]
        public int Id { get; set; }

        [QCFilterField(QSFilterConditionType.Contains, QSFilterConditionType.Equals, Name = "Test 2")]
        [QCFilterField(QSFilterConditionType.Contains, Name = "Test 3")]
        public string Name { get; set; } = null!;

        [QCFilterField(QSFilterConditionType.Greater, Name = "Test 3")]
        public DateTime DateCreate { get; set; }

    }


}
