namespace SalesTrend.WPF.Models;

/// <summary>
/// Товар
/// </summary>
public class Product
{
    public Product()
    {
        
        PriceListProducts = new List<PriceListProduct>();
        ClientOrderProducts = new List<ClientOrderProduct>();
    }

    public int ProductId { get; set; }
    public string Article { get; set; } = null!;
    public string Name { get; set; } = null!;

    public ICollection<PriceListProduct>? PriceListProducts { get; set; }
    public ICollection<ClientOrderProduct>? ClientOrderProducts { get; set; }
}