#nullable disable
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
using System.Windows.Input;

namespace WPF.ViewModels;

public class OwnerViewModel : INotifyPropertyChanged
{
    private OwnerRepository ownerRepository;
    private IEnumerable<Owner> owners;
    private string searchStr = "";
    private string newName;
    private string newSurname;
    private string newNumber;
    private Owner selected;

    public ObservableCollection<Owner> Owners => new(owners.Where(o => o.Name.Contains(SearchStr) || o.Surname.Contains(SearchStr) || o.Number.Contains(SearchStr)));
    public IEnumerable<EntertainmentCenter> Centers => Selected?.EntertainmentCenters;

    public string SearchStr
    {
        get => searchStr;
        set
        {
            searchStr = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Owners));
        }
    }

    public string NewName
    {
        get => newName;
        set
        {
            newName = value;
            OnPropertyChanged();
        }
    }

    public string NewSurname
    {
        get => newSurname;
        set
        {
            newSurname = value;
            OnPropertyChanged();
        }
    }

    public string NewNumber
    {
        get => newNumber;
        set
        {
            newNumber = value;
            OnPropertyChanged();
        }
    }

    public Owner Selected
    {
        get => selected;
        set
        {
            selected = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Centers));
        }
    }

    public OwnerViewModel(DBContext context)
    {
        ownerRepository = new(context);

        owners = ownerRepository.GetItems();
        foreach (var item in owners)
            item.PropertyChanged += Owner_PropertyChanged;
    }

    private void Owner_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace((sender as Owner).Name) || string.IsNullOrWhiteSpace((sender as Owner).Surname) || string.IsNullOrWhiteSpace((sender as Owner).Number))
        {
            MessageBox.Show("Жодне поле не має бути пустим", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            OnPropertyChanged(nameof(Owners));
            return;
        }

        ownerRepository.Update(sender as Owner);
    }

    public ICommand AddOwner => new RelayCommand(AddOwnerExecute);

    private void AddOwnerExecute(object obj)
    {
        if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewSurname) || string.IsNullOrWhiteSpace(NewNumber))
        {
            MessageBox.Show("Жодне поле не має бути пустим", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        ownerRepository.Create(new Owner() { Name = NewName, Surname = NewSurname, Number = NewNumber });
        owners = ownerRepository.GetItems();
        foreach (var item in owners)
            item.PropertyChanged += Owner_PropertyChanged;
        OnPropertyChanged(nameof(Owners));

        NewName = "";
        NewSurname = "";
        NewNumber = "";
    }

    public ICommand DeleteOwner => new RelayCommand(DeleteOwnerExecute);

    private void DeleteOwnerExecute(object obj)
    {
        if ((obj as Owner).EntertainmentCenters.Any())
        {
            MessageBox.Show("Не можна видалити власника, що має хоча б один центр", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        ownerRepository.Delete((obj as Owner).Id);
        owners.ToList().Remove(obj as Owner);
        OnPropertyChanged(nameof(Owners));
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
