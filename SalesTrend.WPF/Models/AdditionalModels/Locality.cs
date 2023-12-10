using System.Collections.Generic;

namespace SalesTrend.WPF.Models;

public class Locality
{
    public Locality()
    {
        Addresses = new List<Address>();
    }

    public int LocalityId { get; set; }
    public string Name { get; set; } = null!;
    public string? ShortName { get; set; }
    public int LocalityTypeId { get; set; }

    public LocalityType LocalityType { get; set; } = null!;
    public ICollection<Address>? Addresses { get; set; }

}
