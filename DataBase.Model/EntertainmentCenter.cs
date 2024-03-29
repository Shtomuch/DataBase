﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Model
{
    public class EntertainmentCenter : INotifyPropertyChanged
    {
        private string title;
        private string address;
        private Owner owner;
        private bool electroCharge;

        public int Id { get; set; }
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }
        public int OwnerId { get; set; }
        public bool ElectroCharge
        {
            get => electroCharge;
            set
            {
                electroCharge = value;
                OnPropertyChanged();
            }
        }
        public virtual List<Service> Services { get; set; }

        [ForeignKey("OwnerId")]
        public virtual Owner Owner
        {
            get => owner;
            set
            {
                owner = value;
                OnPropertyChanged();
                OwnerId = owner.Id;
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
