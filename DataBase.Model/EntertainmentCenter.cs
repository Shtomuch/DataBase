using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Model
{
    public class EntertainmentCenter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public virtual Owner Owner { get; set; }
        public int ParkingCount { get; set; }
        public virtual List<Services> Services { get;  set; }
    }
}
