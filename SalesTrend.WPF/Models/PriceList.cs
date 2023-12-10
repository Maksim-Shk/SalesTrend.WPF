namespace SalesTrend.WPF.Models;

/// <summary>
/// Прайс лист
/// </summary>
public class PriceList
{
    public PriceList()
    {
        PriceListProducts = new HashSet<PriceListProduct>();
    }

    public int PriceListId { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Guid CompanyId { get; set; }

    public Company Company { get; set; } = null!;
    public ICollection<PriceListProduct>? PriceListProducts { get; set; }

}
