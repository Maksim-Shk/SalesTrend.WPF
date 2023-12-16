namespace SalesTrend.WPF.Models;

/// <summary>
/// Прайс лист
/// </summary>
public class ProductType
{
    public ProductType()
    {
        Products = new HashSet<Product>();
    }

    public int ProductTypeId { get; set; }

    public string Name { get; set; } = null!;
    public string? ShortName { get; set; } 

    public ICollection<Product>? Products { get; set; }

}
