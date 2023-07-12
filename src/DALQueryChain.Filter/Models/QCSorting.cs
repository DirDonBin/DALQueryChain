using DALQueryChain.Filter.Enums;

namespace DALQueryChain.Filter.Models
{
    public record QCSorting
    {
        public required string Property { get; set; }
        public QCSortingType Ordering { get; set; } = QCSortingType.Ascending;
    }
}
