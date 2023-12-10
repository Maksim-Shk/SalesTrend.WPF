namespace SalesTrend.WPF.Models;

/// <summary>
/// Позиция в заказе клиента
/// </summary>
public class ClientOrderProduct
{
    public int ClientOrderProductId { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int ClientOrderId { get; set; }

    public Product Product { get; set; } = null!;
    public ClientOrder ClientOrder { get; set; } = null!;
}