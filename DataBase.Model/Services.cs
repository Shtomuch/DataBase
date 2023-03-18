using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Model
{
    public class Services
    {
        public int Id { get; set; } 
        public string BrandName { get; set; }

        public string Descriptions { get; set; }

        public float Cost { get; set; }

        public int Floor { get; set; }

        public virtual EntertainmentCenter EntertainmentCenters { get; set; }
    }
}
