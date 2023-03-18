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
using System.Windows.Input;

namespace DataBase.Winforms
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand AddCommand => new RelayCommand(AddOwner);

        private OwnerRepository Repository = new();

        public ObservableCollection<Owner> Data => new(Repository.GetItems());
        
        private void AddOwner(object owner)
        {

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
}
