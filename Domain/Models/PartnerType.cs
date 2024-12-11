using System;
using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Models;

public partial class PartnerType : EntityBase
{
    public int Id { get; set; }

    public string? PartnerTypeName { get; set; }

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}
