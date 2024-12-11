using System;
using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Models;

public partial class Partner : EntityBase
{
    public int Id { get; set; }

    public int IdType { get; set; }

    public int IdCity { get; set; }

    public int IdStreet { get; set; }

    public string? Name { get; set; }

    public int? House { get; set; }

    public string? Inn { get; set; }

    public string? DirFam { get; set; }

    public string? DirName { get; set; }

    public string? DirOtc { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? Rating { get; set; }

    public decimal? Discount { get; set; }

    public virtual City IdCityNavigation { get; set; } = null!;

    public virtual Street IdStreetNavigation { get; set; } = null!;

    public virtual PartnerType IdTypeNavigation { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
