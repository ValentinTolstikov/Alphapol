using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Models;

public partial class City : EntityBase
{

    public string? CityName { get; set; }

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}
