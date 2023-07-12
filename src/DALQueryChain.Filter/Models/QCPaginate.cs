using System.ComponentModel.DataAnnotations;

namespace DALQueryChain.Filter.Models
{
    public record QCPaginate
    {
        [Range(1, int.MaxValue)]
        public required int Page { get; set; }
        [Range(1, int.MaxValue)]
        public required int PageSize { get; set; }
    }
}
