namespace Domain.Models;

public partial class User : EntityBase
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Otc { get; set; }

    public byte[]? Photo { get; set; }
}
