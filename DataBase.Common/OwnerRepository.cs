using DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Common
{
    public class OwnerRepository : IRepository<Owner>
    {
        

        public void Create(Owner Item)
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

        public Owner GetItem(int id) => GetItems().SingleOrDefault(p => p.Id == id);

        public IEnumerable<Owner> GetItems()
        {
            using var db = new DBContext();
            return db.Owners;
        }

        public void Update(Owner Item)
        {
            using var db = new DBContext();
            db.Update(Item);
            db.SaveChanges();
        }
    }
}
