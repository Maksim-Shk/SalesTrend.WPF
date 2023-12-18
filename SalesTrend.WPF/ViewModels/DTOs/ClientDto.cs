namespace SalesTrend.WPF.ViewModels
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string ClientFullName { get; set; } = null!;
        public string ClientType { get; set; } = null!;
    }
}