using Accord.Statistics.Kernels;
using Microsoft.EntityFrameworkCore;
using SalesTrend.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SalesTrend.WPF.ViewModels;

public class ClientOrderEditViewModel
{

    private ClientOrderDto _selectedClientOrder;

    private ObservableCollection<ClientDto> _Clients;
    public ObservableCollection<ClientDto> Clients
    {
        get
        {
            return _Clients;
        }
        set
        {
            _Clients = value;
            OnPropertyChanged("Clients");
        }
    }

    private ObservableCollection<ProductDto> _Products;
    public ObservableCollection<ProductDto> Products
    {
        get
        {
            return _Products;
        }
        set
        {
            _Products = value;
            OnPropertyChanged("Products");
        }
    }

    private ProductDto _SelectedProduct;
    public ProductDto SelectedProduct
    {
        get => _SelectedProduct;
        set
        {
            if (_SelectedProduct != value)
            {
                _SelectedProduct = value;
                OnPropertyChanged();
            }
        }
    }

    private ClientDto _SelectedClient;
    public ClientDto SelectedClient
    {
        get => _SelectedClient;
        set
        {
            if (_SelectedClient != value)
            {
                _SelectedClient = value;
                OnPropertyChanged();
            }
        }
    }

    public class OrderDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
    }

    private ObservableCollection<OrderDetail> _OrderDetails;
    public ObservableCollection<OrderDetail> OrderDetails
    {
        get
        {
            return _OrderDetails;
        }
        set
        {
            _OrderDetails = value;
            OnPropertyChanged("OrderDetails");
        }
    }


    private RelayCommand _AddToOrderCommand;
    public RelayCommand AddToOrderCommand
    {
        get
        {
            return _AddToOrderCommand ??
              (_AddToOrderCommand = new RelayCommand(obj =>
              {
                  using (var db = new SalesTrendContext())
                  {
                      if (SelectedProduct != null)
                          OrderDetails.Add(new OrderDetail { Name = SelectedProduct.ProductName, Quantity = 1, Id = SelectedProduct.ProductId });
                  }
              }));
        }
    }



    public ObservableCollection<ProductDto> AvailableProducts { get; set; } // Предполагается, что у вас есть список продуктов

    public ClientOrderDto SelectedClientOrder
    {
        get => _selectedClientOrder;
        set
        {
            _selectedClientOrder = value;
            OnPropertyChanged(nameof(SelectedClientOrder));
        }
    }

    private void LoadClients()
    {
        using (var db = new SalesTrendContext())
        {
            var clients = new List<ClientDto>();

            // Получаем клиентов из таблицы Individual
            var individualClients = db.Individuals.Select(i => new ClientDto
            {
                ClientId = i.IndividualId,
                ClientFullName = i.Surname + " " + i.Name + " " + i.Patronymic,
                ClientType = "Физ. лицо"
            });

            clients.AddRange(individualClients);

            // Получаем клиентов из таблицы LegalEntity
            var legalEntityClients = db.LegalEntities.Select(l => new ClientDto
            {
                ClientId = l.LegalEntityId,
                ClientFullName = l.Name,
                ClientType = "Юр. лицо"
            });

            clients.AddRange(legalEntityClients);

            Clients = new ObservableCollection<ClientDto>(clients);
        }
    }

    private void LoadProducts()
    {
        using (var db = new SalesTrendContext())
        {
            var products = db.Products
                .Select(product => new ProductDto
                {
                    ProductName = product.Name,
                    ProductId = product.ProductId,
                    Quantity = 0, // Уточните, откуда брать количество
                    Price = product.PriceListProducts
                        .OrderByDescending(plp => plp.PriceList.ReleaseDate)
                        .FirstOrDefault() != null
                            ? product.PriceListProducts
                                .OrderByDescending(plp => plp.PriceList.ReleaseDate)
                                .FirstOrDefault().Price
                            : 0,
                    Article = product.Article,
                    ProductType = product.ProductType.Name
                }).ToList();

            Products = new ObservableCollection<ProductDto>(products);
        }
    }





    public ClientOrderEditViewModel()
    {
        LoadClients();
        LoadProducts();
        OrderDetails = new ObservableCollection<OrderDetail>();

    }

    public ClientOrderEditViewModel(int clientOriderListId)
    {

        using (var db = new SalesTrendContext())
        {
        }
    }

    private RelayCommand _SaveCommand;
    public RelayCommand SaveCommand
    {
        get
        {
            return _SaveCommand ??
              (_SaveCommand = new RelayCommand(obj =>
              {
                  using (var db = new SalesTrendContext())
                  {
                      if (OrderDetails != null && SelectedClient != null)
                      {
                          var clientOrder = new ClientOrder
                          {
                              OrderDate = DateTime.UtcNow,
                              ClientId = SelectedClient.ClientId

                          };
                          db.ClientOrders.Add(clientOrder);
                          db.SaveChanges();
                          foreach (var orderedProduct in OrderDetails)
                          {
                              var clientOrderProduct = new ClientOrderProduct
                              {
                                  Quantity = orderedProduct.Quantity,
                                  ProductId = orderedProduct.Id,
                                  ClientOrderId = clientOrder.ClientOrderId
                              };
                              db.ClientOrderProducts.Add(clientOrderProduct);
                          }
                          db.SaveChanges();
                      }
                  }
              }));
        }
    }

    private RelayCommand _RemoveCommand;
    public RelayCommand RemoveCommand
    {
        get
        {
            return _RemoveCommand ??
              (_RemoveCommand = new RelayCommand(obj =>
              {
                  using (var db = new SalesTrendContext())
                  {
                      using (var transaction = db.Database.BeginTransaction())
                      {
                          try
                          {
                              var product = db.Products
                                .FirstOrDefault(x => x.ProductId == SelectedProduct.ProductId);

                              if (product != null)
                                  db.Products.Remove(product);
                              else transaction.Rollback();

                              db.SaveChanges();
                              transaction.Commit();
                          }
                          catch
                          {
                              transaction.Rollback();
                          }
                      }
                  }
              }));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
