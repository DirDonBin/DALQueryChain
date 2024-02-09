using DALQueryChain.Interfaces;
using System;
using System.Collections.Generic;

namespace EntityFramework.DAL;

public partial class Category : IDbModelBase
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual ICollection<ArchiveProduct> ArchiveProducts { get; set; } = new List<ArchiveProduct>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
