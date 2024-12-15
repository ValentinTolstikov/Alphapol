using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Sale : EntityBase
{
    public int Id { get; set; }

    public int IdProduct { get; set; }

    public int IdPartner { get; set; }

    public int IdSalePlace { get; set; }

    public int? Count { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Partner IdPartnerNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual SalePlace IdSalePlaceNavigation { get; set; } = null!;
}
