#nullable disable
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataBase.Model
{
    public class Owner : INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private string number;

        public int Id { get; set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged();
            }
        }

        public string Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

        public virtual List<EntertainmentCenter> EntertainmentCenters { get; set; }

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