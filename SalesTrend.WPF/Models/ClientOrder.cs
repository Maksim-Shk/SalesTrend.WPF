namespace SalesTrend.WPF.Models;

/// <summary>
/// Заказ клиента
/// </summary>
public class ClientOrder
{
    public ClientOrder()
    {
        ClientOrderProducts = new List<ClientOrderProduct>();
    }

    public int ClientOrderId { get; set; }

    /// <summary>
    /// LegalEntity / Individual
    /// </summary>
    public Guid ClientId { get; set; }
    public DateTime OrderDate { get; set; }

    public Individual? Individual { get; set; }
    public LegalEntity? LegalEntity { get; set; }
    public ICollection<ClientOrderProduct>? ClientOrderProducts { get; set; }
}