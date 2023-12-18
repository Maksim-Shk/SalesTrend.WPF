using Microsoft.EntityFrameworkCore;
using SalesTrend.WPF.Models;
using SalesTrend.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SalesTrend.WPF.ViewModels;

public class OrderViewModel : INotifyPropertyChanged
{
    private ProductDto _SelectedProduct;

    public ProductDto SelectedProduct
    {
        get
        {
            return _SelectedProduct;
        }
        set
        {
            _SelectedProduct = value;
            OnPropertyChanged(nameof(SelectedProduct));
        }
    }


    private ObservableCollection<ClientOrderDto> _ClientOrders;
    public ObservableCollection<ClientOrderDto> ClientOrders
    {
        get
        {
            return _ClientOrders;
        }
        set
        {
            _ClientOrders = value;
            OnPropertyChanged("ClientOrders");
        }
    }


    private RelayCommand _LoadClientOrdersCommand;
    public RelayCommand LoadClientOrdersCommand
    {
        get
        {
            return _LoadClientOrdersCommand ??
              (_LoadClientOrdersCommand = new RelayCommand(obj =>
              {
                  ClientOrders = new ObservableCollection<ClientOrderDto>();
                  using (var db = new SalesTrendContext())
                  {
                      var clientOrdersDto = db.ClientOrders
                             .Select(co => new ClientOrderDto
                             {
                                 ClientOrderId = co.ClientOrderId,
                                 ClientFullName = co.Individual != null ? $"{co.Individual.Surname} {co.Individual.Name} {(co.Individual).Patronymic}" :
                                                  co.LegalEntity != null ? (co.LegalEntity).Name : string.Empty,
                                 ClientType = co.Individual != null ? "Физическое лицо" :
                                              co.LegalEntity != null ? "Юридическое лицо" : string.Empty,
                                 TotalPrice = co.ClientOrderProducts
                                    .SelectMany(op => op.Product.PriceListProducts
                                        .Where(plp => plp.ProductId == op.ProductId)
                                        .OrderByDescending(plp => plp.PriceList.ReleaseDate)
                                        .Take(1)
                                        .DefaultIfEmpty()
                                        .Select(plp => plp.Price * op.Quantity))
                                    .Sum(),
                                 Products = co.ClientOrderProducts.Select(op => new ProductDto
                                 {
                                     ProductName = op.Product.Name,
                                     ProductId = op.Product.ProductId,
                                     Quantity = op.Quantity,
                                     Price = op.Product.PriceListProducts
                                         .OrderByDescending(plp => plp.PriceList.ReleaseDate)
                                         .FirstOrDefault(plp => plp.ProductId == op.ProductId) != null
                                             ? op.Product.PriceListProducts
                                                 .OrderByDescending(plp => plp.PriceList.ReleaseDate)
                                                 .FirstOrDefault(plp => plp.ProductId == op.ProductId).Price
                                             : 0,
                                     Article = op.Product.Article,
                                     ProductType = op.Product.ProductType.Name
                                 }).ToList()
                             }).ToList();

                      ClientOrders = new ObservableCollection<ClientOrderDto>(clientOrdersDto);
                  }
              }));
        }
    }

    #region commands

    private RelayCommand _RemoveClientOrderCommand;
    public RelayCommand RemoveClientOrderCommand
    {
        get
        {
            return _RemoveClientOrderCommand ??
                (_RemoveClientOrderCommand = new RelayCommand(obj =>
                {
                    if (obj is ClientOrderDto selectedClientOrder)
                    {
                        using (var db = new SalesTrendContext())
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var clientOrder = db.ClientOrders
                                        .FirstOrDefault(co => co.ClientOrderId == selectedClientOrder.ClientOrderId);
                                    db.ClientOrders.Remove(clientOrder);
                                    db.SaveChanges();

                                    transaction.Commit();
                                }
                                catch
                                {
                                    transaction.Rollback();
                                }
                            }
                        }
                    }
                }));
        }
    }

    private RelayCommand _UpdateClientOrderCommand;
    public RelayCommand UpdateClientOrderCommand
    {
        get
        {
            return _UpdateClientOrderCommand ??
                (_UpdateClientOrderCommand = new RelayCommand(obj =>
                {
                    if (SelectedProduct != null)
                    {
                        var window = new ClientOrderEditWindow()
                        {

                        };
                    }
                }));
        }
    }

    private RelayCommand _AddClientOrderCommand;
    public RelayCommand AddClientOrderCommand
    {
        get
        {
            return _AddClientOrderCommand ??
                (_AddClientOrderCommand = new RelayCommand(obj =>
                {
                    var clientOrderEditWindow = new ClientOrderEditWindow();
                    clientOrderEditWindow.Show();
                }));
        }
    }



    #endregion

    public OrderViewModel()
    {
        using (var db = new SalesTrendContext())
        {
            LoadClientOrdersCommand.Execute(null);
            //SelectedCompany = db.Companies.ToList()[0];
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
