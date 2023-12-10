namespace SalesTrend.WPF.Models;

/// <summary>
/// Юр лицо
/// </summary>
public class LegalEntity
{
    public LegalEntity()
    {
        Phones = new HashSet<Phone>();
        Addresses = new HashSet<Address>();
    }

    public Guid LegalEntityId { get; set; }
    public string Name { get; set; } = null!;
    public string? ShortName { get; set; }
    public int TaxNumber { get; set; }
    public string? ContactPersonFullName { get; set; }
    public string? Email { get; set; } = null!;

    public ICollection<Phone>? Phones { get; set; }
    public ICollection<Address>? Addresses { get; set; }
}