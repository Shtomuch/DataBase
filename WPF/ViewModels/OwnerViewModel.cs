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
using System.Text.RegularExpressions;
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
    private string newPhone;
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

    public string NewPhone
    {
        get => newPhone;
        set
        {
            value = value.TrimStart();

            if (newPhone?.Length > value.Length)
            {
                newPhone = value.Trim(" -".ToCharArray());
                OnPropertyChanged();
                return;
            }

            if (Regex.IsMatch(value, @"\d+ \d+ \d{4}") || Regex.IsMatch(value, @"\d+ \d+ \d{3}-\d{3}"))
                value = $"{value[..^1]}-{value.Last()}";
            else if (Regex.IsMatch(value, @"\d+ \d+ \d{3}[^0-9]$") || Regex.IsMatch(value, @"\d+ \d+ \d{3}-\d{2}[^0-9]$"))
                value = value.Trim();
            else if (Regex.IsMatch(value, @"\d+ \d+ \d{3}-\d{2}-\d{2}.+"))
                value = Regex.Match(value, @"\d+ \d+ \d{3}-\d{2}-\d{2}").Value;

            newPhone = value;

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
        if (!Regex.IsMatch((sender as Owner).Number, @"\d+ \d+ \d{3}-\d{2}-\d{2}"))
        {
            MessageBox.Show("Невірно введений номер", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            OnPropertyChanged(nameof(Owners));
            return;
        }

        ownerRepository.Update(sender as Owner);
    }

    public ICommand AddOwner => new RelayCommand(AddOwnerExecute);

    private void AddOwnerExecute(object obj)
    {
        if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewSurname) || string.IsNullOrWhiteSpace(NewPhone))
        {
            MessageBox.Show("Жодне поле не має бути пустим", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (!Regex.IsMatch(NewPhone, @"\d+ \d+ \d{3}-\d{2}-\d{2}"))
        {
            MessageBox.Show("Невірно введений номер", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            OnPropertyChanged(nameof(Owners));
            return;
        }

        ownerRepository.Create(new Owner() { Name = NewName, Surname = NewSurname, Number = NewPhone });
        owners = ownerRepository.GetItems();
        foreach (var item in owners)
            item.PropertyChanged += Owner_PropertyChanged;
        OnPropertyChanged(nameof(Owners));

        NewName = "";
        NewSurname = "";
        NewPhone = "";
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
