using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Model
{
    public class DBContext: DbContext
    {
        public DbSet<EntertainmentCenter> EntertainmentCenters { get; set; }
       
        public DbSet<Owner> Owners{ get; set; }

        public DbSet<Services> Service { get; set; }

        public string DbPath => "DataBase.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Data Source={DbPath}");
        }

    }
}
