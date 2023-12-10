namespace SalesTrend.WPF.Models
{
    public class Phone
    {
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }

        // Общее свойство для хранения CompanyId или LegalEntityId
        public Guid EntityId { get; set; }

        public Company? Company { get; set; }
        public LegalEntity? LegalEntity { get; set; }
    }
}