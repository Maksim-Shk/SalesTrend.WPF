using Microsoft.EntityFrameworkCore;
using Npgsql;
using SalesTrend.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static SalesTrend.WPF.ViewModels.ClientOrderEditViewModel;

namespace SalesTrend.WPF.ViewModels;

public class SalesTrendViewModel : INotifyPropertyChanged
{
    private DateTime _Date;
    public DateTime Date
    {
        get
        {
            return _Date;
        }
        set
        {
            _Date = value;
            OnPropertyChanged(nameof(Date));
        }
    }

    private string _ProductName;
    public string ProductName
    {
        get
        {
            return _ProductName;
        }
        set
        {
            _ProductName = value;
            OnPropertyChanged(nameof(ProductName));
        }
    }

    private int _RequiredQuantity;
    public int RequiredQuantity
    {
        get
        {
            return _RequiredQuantity;
        }
        set
        {
            _RequiredQuantity = value;
            OnPropertyChanged(nameof(RequiredQuantity));
        }
    }

    private ObservableCollection<ClientViewDto> _ClientViewDtos;
    public ObservableCollection<ClientViewDto> ClientViewDtos
    {
        get
        {
            return _ClientViewDtos;
        }
        set
        {
            _ClientViewDtos = value;
            OnPropertyChanged("ClientViewDtos");
        }
    }

    private string _productTypeName;
    public string ProductTypeName
    {
        get { return _productTypeName; }
        set
        {
            _productTypeName = value;
            OnPropertyChanged(nameof(ProductTypeName));
        }
    }
    private ObservableCollection<ProductViewDto> _productViewDtos;
    public ObservableCollection<ProductViewDto> ProductViewDtos
    {
        get { return _productViewDtos; }
        set
        {
            _productViewDtos = value;
            OnPropertyChanged(nameof(ProductViewDtos));
        }
    }


    public class ClientViewDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class ProductViewDto
    {
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public decimal ProductPrice { get; set; }
    }

    private RelayCommand _Query1Command;
    public RelayCommand Query1Command
    {
        get
        {
            return _Query1Command ??
              (_Query1Command = new RelayCommand(obj =>
              {
                  using (var db = new SalesTrendContext())
                  {
                      string query = @"
                        SELECT DISTINCT c.""Name"", op.""Quantity"", o.""OrderDate"", op.""ClientOrderId""
                        FROM ""Individual"" c
                        INNER JOIN ""ClientOrder"" o ON c.""IndividualId"" = o.""ClientId""
                        INNER JOIN ""ClientOrderProduct"" op ON o.""ClientOrderId"" = op.""ClientOrderId""
                        INNER JOIN ""Product"" p ON op.""ProductId"" = p.""ProductId""
                        WHERE o.""OrderDate"" > :TargetDate
                            AND p.""Name"" = :ProductName
                            AND op.""Quantity"" >= :RequiredQuantity";

                      ClientViewDtos = new ObservableCollection<ClientViewDto>(db.ClientOrderProducts
                          .FromSqlRaw(query,
                              new NpgsqlParameter("TargetDate", Date),
                              new NpgsqlParameter("ProductName", ProductName),
                              new NpgsqlParameter("RequiredQuantity", RequiredQuantity))
                          .Select(op => new ClientViewDto
                          {
                              Name = op.ClientOrder.Individual.GetFullName(),
                              Quantity = op.Quantity,
                              OrderDate = op.ClientOrder.OrderDate
                          })
                          .Distinct());
                  }
              }));
        }
    }

    private RelayCommand _Query2Command;
    public RelayCommand Query2Command
    {
        get
        {
            return _Query2Command ??
              (_Query2Command = new RelayCommand(obj =>
              {
                  using (var db = new SalesTrendContext())
                  {
                      string query = @"
        SELECT
            p.""Name"" AS ""ProductName"",
            pt.""Name"" AS ""ProductTypeName"",
            latestPrice.""Price"" AS ""ProductPrice""
        FROM
            ""Product"" p
        JOIN
            ""ProductType"" pt ON p.""ProductTypeId"" = pt.""ProductTypeId""
        JOIN (
            SELECT
                DISTINCT ON (plp.""ProductId"")
                plp.""ProductId"",
                pl.""ReleaseDate"",
                plp.""Price""
            FROM
                ""PriceListProduct"" plp
            JOIN
                ""PriceList"" pl ON plp.""PriceListId"" = pl.""PriceListId""
            WHERE
                pl.""ReleaseDate"" <= @TargetDate
            ORDER BY
                plp.""ProductId"",
                pl.""ReleaseDate"" DESC
        ) AS latestPrice ON p.""ProductId"" = latestPrice.""ProductId""
        WHERE
            pt.""Name"" = @ProductTypeName
        ORDER BY
            p.""Name""";



                      ProductViewDtos = new ObservableCollection<ProductViewDto>(db.Products
                          .FromSqlRaw(query,
                              new NpgsqlParameter("TargetDate", Date),
                              new NpgsqlParameter("ProductTypeName", ProductTypeName))
                          .Select(op => new ProductViewDto
                          {
                              ProductName = op.Name,
                              ProductTypeName = op.ProductType.Name,
                              ProductPrice = op.PriceListProducts.Where(x=>x.ProductId==op.ProductId).First().Price
                          }));
                  }
              }));
        }
    }


    public SalesTrendViewModel()
    {
        ProductName = "Ноутбук";
        RequiredQuantity = 4;
        Date = DateTime.UtcNow.AddMonths(-1);
        ProductTypeName = "Прочая электроника";
    }



    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
