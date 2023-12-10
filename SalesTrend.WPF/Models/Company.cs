using System.ComponentModel;

namespace SalesTrend.WPF.Models;

/// <summary>
/// Предприятие
/// </summary>
public class Company
{
    public Company()
    {
        Phones = new HashSet<Phone>();
        PriceLists = new HashSet<PriceList>();
        Addresses = new HashSet<Address>();
    }

    public Guid CompanyId { get; set; }

    [DisplayName("Наименование")]
    public string Name { get; set; } = null!;

    [DisplayName("Электронная почта")]
    public string? Email { get; set; } = null!;

    [DisplayName("Сайт")]
    public string? Url { get; set; }

    [DisplayName("ФИО контактного лица")]
    public string? ContactPersonFullName { get; set; }

    public ICollection<Phone>? Phones { get; set; }
    public ICollection<PriceList>? PriceLists { get; set; }
    public ICollection<Address>? Addresses { get; set; }
}
