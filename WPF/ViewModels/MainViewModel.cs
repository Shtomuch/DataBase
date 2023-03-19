﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBase.Model;
using Microsoft.EntityFrameworkCore;

namespace WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public DBContext Context { get; } = new();

        public OwnerViewModel Owner { get; }

        public MainViewModel()
        {
            Context.Database.Migrate();

            Owner = new(Context);
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
