using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Partner : EntityBase
{
    public Partner() 
    { }

    public Partner(Partner p)
    {
        Id = p.Id;
        IdType = p.IdType;
        IdCity = p.IdCity;
        IdStreet = p.IdStreet;
        Name = p.Name;
        House = p.House;
        Inn = p.Inn;
        DirFam = p.DirFam;
        DirName = p.DirName;
        DirOtc = p.DirOtc;
        Phone = p.Phone;
        Email = p.Email;
        Rating = p.Rating;
        IdCityNavigation = p.IdCityNavigation;
        IdStreetNavigation = p.IdStreetNavigation;
        IdTypeNavigation = p.IdTypeNavigation;
    }

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

    public PartnerType IdTypeNavigation { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
