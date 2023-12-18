namespace SalesTrend.WPF.ViewModels
{
    public class ClientOrderDto
    {
        public int ClientOrderId { get; set; }
        public string ClientFullName { get; set; } = null!;
        public string ClientType { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public List<ProductDto> Products { get; set; } = null!;
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Article { get; set; } = null!;
        public string ProductType { get; set; } = null!;
    }
}