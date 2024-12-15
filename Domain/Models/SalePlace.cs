using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class SalePlace : EntityBase
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
