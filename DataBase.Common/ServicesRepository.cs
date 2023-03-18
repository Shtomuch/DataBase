using DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Common
{
   public class ServicesRepository : IRepository<Services>
    {


        public void Create(Services Item)
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

        public Services GetItem(int id) => GetItems().SingleOrDefault(p => p.Id == id);

        public IEnumerable<Services> GetItems()
        {
            using var db = new DBContext();
            return db.Service;
        }

        public void Update(Services Item)
        {
            using var db = new DBContext();
            db.Update(Item);
            db.SaveChanges();
        }

    }
}
