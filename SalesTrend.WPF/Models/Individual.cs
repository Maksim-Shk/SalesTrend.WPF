namespace SalesTrend.WPF.Models;

/// <summary>
/// Физ. лицо
/// </summary>
public class Individual
{
    public Guid IndividualId { get; set; }

    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? Patronymic { get; set; }
    //public int AddressId { get; set; }
    public int TaxNumber { get; set; }

    public int PassportSerial { get; set; }
    public int PassportNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public string IssuedBy { get; set; } = null!;

    public Address Address { get; set; } = null!;
}
