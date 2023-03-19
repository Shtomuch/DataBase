using DataBase.Common;
using DataBase.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WPF.ViewModels;

public class SevicesViewModel : INotifyPropertyChanged
{
    private ServicesRepository servicesRepository;
    private EntertainmentCenter center;
    private IEnumerable<Service> services;
    private string newName;
    private string newFloor;
    private string newDescription = "";
    private string searchStr = "";

    public string NewName
    {
        get => newName;
        set
        {
            newName = value;
            OnPropertyChanged();
        }
    }

    public string NewFloor
    {
        get => newFloor;
        set
        {
            newFloor = value;
            OnPropertyChanged();
        }
    }

    public string NewDescription
    {
        get => newDescription;
        set
        {
            newDescription = value;
            OnPropertyChanged();
        }
    }

    public string SearchStr
    {
        get => searchStr;
        set
        {
            searchStr = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Services));
        }
    }

    public ObservableCollection<Service> Services => new(services.Where(s => s.BrandName.Contains(SearchStr) || s.Floor.ToString().Contains(SearchStr) || s.Description.Contains(SearchStr)));


    public SevicesViewModel(DBContext context, EntertainmentCenter center)
    {
        servicesRepository = new(context);
        this.center = center;

        services = servicesRepository.GetItems().Where(s => s.EntertainmentCenterId == center.Id);
        foreach (var item in services)
            item.PropertyChanged += Service_PropertyChanged;
    }

    private void Service_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace((sender as Service).BrandName))
        {
            MessageBox.Show("Поле ім'я не має бути пустим", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            OnPropertyChanged(nameof(Services));
            return;     
        }

        servicesRepository.Update(sender as Service);
    }

    public ICommand AddService => new RelayCommand(AddServiceExecute);

    private void AddServiceExecute(object obj)
    {
        if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewFloor) || !int.TryParse(NewFloor, out int floor) || string.IsNullOrWhiteSpace(NewDescription))
        {
            MessageBox.Show("Жодне поле не має бути пустим", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        servicesRepository.Create(new Service() { BrandName = NewName, Floor = floor, Description = NewDescription, EntertainmentCenterId = center.Id});
        services = servicesRepository.GetItems().Where(s => s.EntertainmentCenterId == center.Id);
        foreach (var item in services)
            item.PropertyChanged += Service_PropertyChanged;
        OnPropertyChanged(nameof(Services));

        NewName = "";
        NewFloor = "";
        NewDescription = "";
    }

    public ICommand DeleteService => new RelayCommand(DeleteServiceExecute);

    private void DeleteServiceExecute(object obj)
    {
        servicesRepository.Delete((obj as Service).Id);
        services.ToList().Remove(obj as Service);
        OnPropertyChanged(nameof(Services));
    }

    public ICommand NewLine => new RelayCommand(NewLineExecute);

    private void NewLineExecute(object obj)
    {
        TextBox textBox = obj as TextBox;
        int caret = textBox.CaretIndex;
        textBox.Text = textBox.Text.Insert(textBox.CaretIndex, Environment.NewLine);
        textBox.CaretIndex = caret + 1;
    }

    #region PropertyChanged
#nullable disable
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
#nullable enable
    #endregion
}
