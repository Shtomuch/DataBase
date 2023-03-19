using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Model
{
    public class Service : INotifyPropertyChanged
    {
        private string name;
        private string description;
        private int floor;
        private EntertainmentCenter center;

        public int Id { get; set; }

        public string BrandName
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public int Floor
        {
            get => floor;
            set
            {
                floor = value;
                OnPropertyChanged();
            }
        }

        public int EntertainmentCenterId { get; set; }

        [BackingField("center")]
        [ForeignKey("EntertainmentCenterId")]
        public virtual EntertainmentCenter EntertainmentCenter
        {
            get => center;
            set
            {
                center = value;
                OnPropertyChanged();
                EntertainmentCenterId = center.Id;
            }
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
