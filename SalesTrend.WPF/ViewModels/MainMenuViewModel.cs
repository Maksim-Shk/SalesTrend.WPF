using SalesTrend.WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows.Controls;
using System.Windows;
using System.Linq;

namespace SalesTrend.WPF.ViewModels;

public class MainMenuViewModel : INotifyPropertyChanged
{
    public MainMenuViewModel()
    {

    }

    private RelayCommand _LoadMainWindowCommand;
    public RelayCommand LoadMainWindowCommand
    {
        get
        {
            return _LoadMainWindowCommand ??
              (_LoadMainWindowCommand = new RelayCommand(obj =>
              {
                  using (var db = new SalesTrendContext())
                  {

                  }
              }));
        }
    }

    private Locality _currentLocality;
    public Locality CurrentLocality
    {
        get => _currentLocality;
        set
        {
            if (_currentLocality != value)
            {
                _currentLocality = value;
                OnPropertyChanged();
            }
        }
    }

    private RelayCommand _addOrUpdateLocalityCommand;
    public RelayCommand AddOrUpdateLocalityCommand
    {
        get
        {
            return _addOrUpdateLocalityCommand ??
              (_addOrUpdateLocalityCommand = new RelayCommand(obj =>
              {
                  using (var db = new SalesTrendContext())
                  {
                      if (CurrentLocality.LocalityId == 0)
                      {
                          db.Localities.Add(CurrentLocality);
                      }
                      else
                      {
                          var existingLocality = db.Localities.FirstOrDefault(l => l.LocalityId == CurrentLocality.LocalityId);

                          if (existingLocality == null)
                          {
                              db.Localities.Add(CurrentLocality);
                          }
                          else
                          {
                              db.Entry(existingLocality).CurrentValues.SetValues(CurrentLocality);
                          }
                      }

                      db.SaveChanges();

                      MessageBox.Show("Locality added or updated successfully!");
                  }
              },
              obj => CurrentLocality != null && !string.IsNullOrEmpty(CurrentLocality.Name))); // The command can execute if the CurrentLocality exists and its Name is not empty.
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
