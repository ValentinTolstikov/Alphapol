using Domain.Interfaces;

namespace Domain.Models;

public partial class MaterialType : EntityBase
{
    public int Id { get; set; }

    public string? MaterialName { get; set; }

    public decimal? Koef { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
