using DALQueryChain.Interfaces;
using System;
using System.Collections.Generic;

namespace EntityFramework.DAL;

public partial class ArchiveProduct : IDbModelBase
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public decimal? Price { get; set; }

    public int? Count { get; set; }

    public int? Categoryid { get; set; }

    public double? Raiting { get; set; }

    public virtual Category? Category { get; set; }
}
