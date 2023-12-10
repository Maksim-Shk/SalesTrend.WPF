namespace SalesTrend.WPF.Models;

/// <summary>
/// Позиция в прайс листе
/// </summary>
public class PriceListProduct
{
    public int ProductId { get; set; }
    public int PriceListId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product Product { get; set; } = null!;
    public PriceList PriceList { get; set; } = null!;
}
