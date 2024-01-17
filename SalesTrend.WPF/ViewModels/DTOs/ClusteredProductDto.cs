namespace SalesTrend.WPF.ViewModels.DTOs;

public class ClusteredProductDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required double Price { get; set; }
    public required double Total { get; set; }
    public required double NormalizedTotal { get; set; }
    public required double NormalizedPrice { get; set; }
    public int? AssignedCluster { get; set; }
}
