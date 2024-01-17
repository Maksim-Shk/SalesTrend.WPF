using Accord.MachineLearning;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using SalesTrend.WPF.Models;
using SalesTrend.WPF;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using SalesTrend.WPF.ViewModels.DTOs;

namespace SalesTrend.WPF.ViewModels;

public class ClusterAnalysisViewModel : INotifyPropertyChanged
{
    public ClusterAnalysisViewModel()
    {
        PlotCollection = new PlotModel { Title = "Кластеризация заказов клиентов" };
        PlotCollection.Background = OxyColors.White;

        UseAlgoritm = true;

        using (var db = new SalesTrendContext())
        {
            Products = new ObservableCollection<Product>(db.Products);
        }
    }

    public ObservableCollection<ClusteredProductDto> ClusteredProducts { get; set; } = new ObservableCollection<ClusteredProductDto>();

    private ObservableCollection<Product> _Products;
    public ObservableCollection<Product> Products
    {
        get { return _Products; }
        set
        {
            if (_Products != value)
            {
                _Products = value;
                OnPropertyChanged("Products");
            }
        }
    }


    private PlotModel _PlotCollection;
    public PlotModel PlotCollection
    {
        get { return _PlotCollection; }
        set
        {
            if (_PlotCollection != value)
            {
                _PlotCollection = value;
                OnPropertyChanged("PlotCollection");
            }
        }
    }

    private RelayCommand _SavePlotCommand;
    public RelayCommand SavePlotCommand
    {
        get
        {
            return _SavePlotCommand ?? (_SavePlotCommand = new RelayCommand(param =>
            {
                var plotView = param as PlotView;
                SavePlotAsPng(plotView);
            }));
        }
    }

    private void SavePlotAsPng(PlotView plotView)
    {
        var dialog = new SaveFileDialog()
        {
            Filter = "PNG Files (*.png)|*.png",
            DefaultExt = ".png",
            AddExtension = true
        };
        if (dialog.ShowDialog() == true)
        {
            string fileName = dialog.FileName;
            var pngExporter = new OxyPlot.Wpf.PngExporter { Width = 600, Height = 400 };

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                pngExporter.Export(plotView.Model, fileStream);
            }
        }
    }

    private bool _UseAlgoritm;

    public bool UseAlgoritm
    {
        get { return _UseAlgoritm; }
        set
        {
            if (_UseAlgoritm != value)
            {
                _UseAlgoritm = value;
                OnPropertyChanged("UseAlgoritm");
            }
        }
    }

    public class SimpleProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public double Total { get; set; }
        public int Cluster { get; set; }
    }

    private RelayCommand _LoadClusterAnalysisWindowCommand;
    public RelayCommand LoadClusterAnalysisWindowCommand
    {
        get
        {
            return _LoadClusterAnalysisWindowCommand ??
              (_LoadClusterAnalysisWindowCommand = new RelayCommand(obj =>
              {
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

                      var rawData = new List<SimpleProduct>();

                      foreach (var clientOrder in clientOrdersDto)
                      {
                          foreach (var product in clientOrder.Products)
                          {
                              rawData.Add(new SimpleProduct
                              {
                                  Id = product.ProductId,
                                  Name = product.ProductName,
                                  Price = (double)product.Price,
                                  Total = product.Quantity
                              });
                          }
                      }

                      //Нормализация данных
                      double maxPrice = rawData.Max(r => Convert.ToDouble(r.Price));
                      double maxTotal = rawData.Max(r => r.Total);

                      var normalizedData = rawData.Select(r => new[]
                      {
                          r.Price / maxPrice,
                          r.Total / maxTotal
                      }).ToList();

                      double[][] observations = normalizedData
                           .Select(r => r.Select(val => val).ToArray())
                           .ToArray();

                      //k-means кластеризация

                      // Задаем количество кластеров
                      int clusterCount = 4;
                      var kmeans = new KMeans(clusterCount) { };

                      // Выполняем кластеризацию
                      var clusters = kmeans.Learn(observations);

                      int[] labels;
                      if (UseAlgoritm is true)
                          labels = clusters.Decide(observations);
                      else
                          labels = rawData.Select(p => p.Cluster).ToArray();

                      ClusteredProducts.Clear();

                      if (UseAlgoritm is true)
                      {
                          for (int i = 0; i < rawData.Count; i++)
                          {
                              ClusteredProducts.Add(new ClusteredProductDto
                              {
                                  Id = rawData[i].Id,
                                  Price = rawData[i].Price,
                                  NormalizedPrice = normalizedData[i][0],
                                  Total = rawData[i].Total,
                                  NormalizedTotal = normalizedData[i][1],
                                  AssignedCluster = labels[i],
                                  Name = rawData[i].Name
                              });

                              rawData[i].Cluster = labels[i];

                              //db.Products.Update(rawData[i]);
                          }
                          db.SaveChanges();
                      }
                      else
                      {
                          var products = rawData;
                          int j = 0;
                          foreach (var product in products)
                          {
                              ClusteredProducts.Add(new ClusteredProductDto
                              {
                                  Id = product.Id,
                                  Price = product.Price,
                                  NormalizedPrice = normalizedData[j][0],
                                  Total = product.Total,
                                  NormalizedTotal = normalizedData[j][1],
                                  AssignedCluster = product.Cluster,
                                  Name = product.Name
                              });
                              j++;
                          }
                      }

                      var groupedByCluster = observations
                               .Zip(labels, (observation, label) => new { observation, label })
                               .GroupBy(ol => ol.label)
                               .ToList();

                      var clusterColors = new[]
                      {
                          OxyColors.Red,
                          OxyColors.Black,
                          OxyColors.Green,
                          OxyColors.Blue,
                          OxyColors.Yellow
                      };

                      foreach (var clusterGroup in groupedByCluster)
                      {
                          var series = new ScatterSeries
                          {
                              MarkerType = MarkerType.Circle,
                              MarkerSize = 4,
                              MarkerFill = clusterColors[clusterGroup.Key]

                          };

                          foreach (var item in clusterGroup)
                          {
                              // Денормализация данных
                              double x = item.observation[0] * maxPrice;
                              double y = item.observation[1] * maxTotal;
                              series.Points.Add(new ScatterPoint(x, y));
                          }

                          AddSeriesToPlot(_PlotCollection, series);
                      }

                      OnPropertyChanged("PlotCollection");
                      PlotCollection.InvalidatePlot(true);
                  }
              }));
        }
    }
    private void AddSeriesToPlot(PlotModel plotModel, ScatterSeries series)
    {
        plotModel.Series.Add(series);
        OnPropertyChanged("PlotCollection");
    }

    private RelayCommand _ToggleAlgorithmCommand;
    public RelayCommand ToggleAlgorithmCommand
    {
        get
        {
            return _ToggleAlgorithmCommand ?? (_ToggleAlgorithmCommand = new RelayCommand(obj =>
            {
                //MessageBox.Show("Данные не прошли проверку целостности!","Ошибка");
                UseAlgoritm = !UseAlgoritm;
                MessageBox.Show(UseAlgoritm.ToString());
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
