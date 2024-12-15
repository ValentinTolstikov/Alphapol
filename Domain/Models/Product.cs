using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Product : EntityBase
{
    public int Id { get; set; }

    public int? IdMaterialType { get; set; }

    public string? Name { get; set; }

    public decimal? MinCost { get; set; }

    public virtual MaterialType? IdMaterialTypeNavigation { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
