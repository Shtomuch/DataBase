#nullable disable
using DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DataBase.Common;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Input;

namespace WPF.ViewModels;

public class CentersViewModel : INotifyPropertyChanged
{
    private EntertainmentCenterRepository centerRepository;
    private OwnerRepository ownerRepository;
    private IEnumerable<EntertainmentCenter> centers;
    private string searchStr = "";
    private string newTitle;
    private string newAddress;
    private Owner newOwner;
    private bool newElectroCharge;
    private EntertainmentCenter selected;
    private bool tabSelected;
    private SevicesViewModel sevicesViewModel;
    private DBContext context;

    public string SearchStr
    {
        get => searchStr;
        set
        {
            searchStr = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Centers));
        }
    }

    public string NewTitle
    {
        get => newTitle;
        set
        {
            newTitle = value;
            OnPropertyChanged();
        }
    }

    public string NewAddress
    {
        get => newAddress;
        set
        {
            newAddress = value;
            OnPropertyChanged();
        }
    }

    public Owner NewOwner
    {
        get => newOwner;
        set
        {
            newOwner = value;
            OnPropertyChanged();
        }
    }

    public bool NewElectroCharge
    {
        get => newElectroCharge;
        set
        {
            newElectroCharge = value;
            OnPropertyChanged();
        }
    }

    public EntertainmentCenter Selected
    {
        get => selected;
        set
        {
            selected = value;
            OnPropertyChanged();
            SevicesViewModel = new(context, selected);
        }
    }

    public bool TabSelected
    {
        get => tabSelected;
        set
        {
            tabSelected = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Owners));
        }
    }

    public SevicesViewModel SevicesViewModel
    {
        get => sevicesViewModel;
        set
        {
            sevicesViewModel = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<EntertainmentCenter> Centers => new(centers.Where(e => e.Title.Contains(SearchStr) || e.Address.Contains(SearchStr) || e.Owner.Name.Contains(SearchStr)));


    public IEnumerable<Owner> Owners => ownerRepository.GetItems().ToList();

    public CentersViewModel(DBContext context)
    {
        centerRepository = new(context);
        ownerRepository = new(context);

        this.context = context;

        centers = centerRepository.GetItems();
        foreach (var item in centers)
            item.PropertyChanged += Center_PropertyChanged;
    }

    private void Center_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace((sender as EntertainmentCenter).Title) || string.IsNullOrWhiteSpace((sender as EntertainmentCenter).Address))
        {
            MessageBox.Show("Жодне поле не має бути пустим", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            OnPropertyChanged(nameof(Centers));
            return;
        }

        centerRepository.Update(sender as EntertainmentCenter);
    }

    public ICommand AddCenter => new RelayCommand(AddCenterExecute);

    private void AddCenterExecute(object obj)
    {
        if (string.IsNullOrWhiteSpace(NewTitle) || string.IsNullOrWhiteSpace(NewAddress))
        {
            MessageBox.Show("Жодне поле не має бути пустим", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        centerRepository.Create(new EntertainmentCenter() { Title = NewTitle, Address = NewAddress, OwnerId = NewOwner.Id, ElectroCharge = NewElectroCharge});
        centers = centerRepository.GetItems();
        foreach (var item in centers)
            item.PropertyChanged += Center_PropertyChanged; 
        OnPropertyChanged(nameof(Centers));

        NewTitle = "";
        NewAddress = "";
    }

    public ICommand DeleteCenter => new RelayCommand(DeleteCenterExecute);

    private void DeleteCenterExecute(object obj)
    {
        centerRepository.Delete((obj as EntertainmentCenter).Id);
        centers.ToList().Remove(obj as EntertainmentCenter);
        OnPropertyChanged(nameof(Centers));
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
