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
    [QCFilter(Name = "Test 4")]
    public class TestModel
    {
        [QCFilterField(QSFilterConditionType.Equals)]
        [QCFilterField(QSFilterConditionType.Greater, Name = "Test 1")]
        public int Id { get; set; }

        [QCFilterField(QSFilterConditionType.Contains, QSFilterConditionType.Equals, Name = "Test 2")]
        [QCFilterField(QSFilterConditionType.Contains, Name = "Test 3")]
        public string Name { get; set; } = null!;

        [QCFilterField(QSFilterConditionType.Contains, Name = "Test 4", FieldName = nameof(TestSimpleModel.Id))]
        public List<int>? Ids { get; set; }

        [QCFilterField(QSFilterConditionType.Greater, Name = "Test 3")]
        public DateTime DateCreate { get; set; }

    }

    public class TestSimpleModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateCreate { get; set; }

        public TestAssociate1? Associate { get; set; }
    }

    public class TestAssociate1
    {
        public int Id { get; set; }
        public List<TestAssociate2> Associates { get; set; } = null!;
    }

    public class TestAssociate2
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class TestHuitest
    {
        public int Id { get; set; }
        public FilterModel<int> Status { get; set; } = null!;
    }

    public class FilterModel<T>
    {
        
        public T? Value { get; set; }
        public QSFilterConditionType? Condition { get; set; }
    }
}
