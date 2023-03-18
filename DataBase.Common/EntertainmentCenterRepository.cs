using DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Common
{
    internal class EntertainmentCenterRepository : IRepository<EntertainmentCenter>
    {
        

        public void Create(EntertainmentCenter Item)
        {
            using var db = new DBContext();
            db.Add(Item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            using var db = new DBContext();
            db.Remove(GetItem(id));
            db.SaveChanges();
        }

        public EntertainmentCenter GetItem(int id) => GetItems().SingleOrDefault(p => p.Id == id);

        public IEnumerable<EntertainmentCenter> GetItems()
        {
            using var db = new DBContext();
            return db.EntertainmentCenters;
        }

        public void Update(EntertainmentCenter Item)
        {
            using var db = new DBContext();
            db.Update(Item);
            db.SaveChanges();
        }
    }
}
