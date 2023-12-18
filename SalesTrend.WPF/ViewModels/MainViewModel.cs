namespace SalesTrend.WPF.ViewModels;

public class MainViewModel
{
    public MainMenuViewModel MainMenuViewModel { get; set; }

    public OrderViewModel OrderViewModel { get; set; }
    public ClientOrderEditViewModel ClientOrderEditViewModel { get; set; }

    public SalesTrendViewModel SalesTrendViewModel { get; set; }

    public MainViewModel()
    {
        MainMenuViewModel = new MainMenuViewModel();
        OrderViewModel = new OrderViewModel();
        ClientOrderEditViewModel=  new ClientOrderEditViewModel();
        SalesTrendViewModel = new SalesTrendViewModel();
    }
}