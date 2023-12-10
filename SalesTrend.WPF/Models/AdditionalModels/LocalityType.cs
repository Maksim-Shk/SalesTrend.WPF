using System.Collections.Generic;

namespace SalesTrend.WPF.Models;

public class LocalityType
{
    public LocalityType()
    {
        Localities = new List<Locality>(); 
    }

    public int LocalityTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? ShortName { get; set; }

    public ICollection<Locality>? Localities { get; set; }
}
