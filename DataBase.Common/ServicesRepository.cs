#nullable disable
using DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Common
{
   public class ServicesRepository : IRepository<Service>
    {
        public DBContext Context { get; init; }

        public ServicesRepository(DBContext context) => Context = context;

        public void Create(Service Item)
        {
            Context.Add(Item);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            Context.Remove(GetItem(id));
            Context.SaveChanges();
        }

        public Service GetItem(int id) => GetItems().SingleOrDefault(p => p.Id == id);

        public IEnumerable<Service> GetItems()
        {
            return Context.Service;
        }

        public void Update(Service Item)
        {
            Context.Update(Item);
            Context.SaveChanges();
        }

    }
}
