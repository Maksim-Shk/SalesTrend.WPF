namespace SalesTrend.WPF.Models
{
    public class Street
    {
        public Street()
        {
            Addresses = new List<Address>();
        }
        public int StreetId { get; set; }
        public string Name { get; set; } = null!;
        public string? ShortName { get; set; }
        public int StreeTypetId { get; set; }

        public StreetType StreetType { get; set; } = null!;
        public ICollection<Address>? Addresses { get; set; } // Добавлено свойство Addresses
    }
}
