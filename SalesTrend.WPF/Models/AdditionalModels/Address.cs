using System;

namespace SalesTrend.WPF.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        public string? House { get; set; }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string? Apartment { get; set; }

        /// <summary>
        /// Ид населенного пункта
        /// </summary>
        public int LocalityId { get; set; }

        /// <summary>
        /// Ид улицы
        /// </summary>
        public int StreetId { get; set; }

        ///// <summary>
        ///// Ид организации / компании
        ///// </summary>
        public Guid CompanyId { get; set; }
        public Locality Locality { get; set; } = null!;
        public Street Street { get; set; } = null!;

        public Company? Company { get; set; }
        public Individual? Individual { get; set; }
        public LegalEntity? LegalEntity { get; set; }
    }
}
