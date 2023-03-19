#nullable disable
using DataBase.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Common
{
    public class OwnerRepository : IRepository<Owner>
    {
        public DBContext Context { get; init; }

        public OwnerRepository(DBContext context) => Context = context;

        public void Create(Owner Item)
        {
            Context.Add(Item);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            Context.Remove(GetItem(id));
            Context.SaveChanges();
        }

        public Owner GetItem(int id) => GetItems().SingleOrDefault(p => p.Id == id);

        public IEnumerable<Owner> GetItems()
        {
            return Context.Owners;
        }

        public void Update(Owner Item)
        {
            Context.Update(Item);
            Context.SaveChanges();
        }
    }
}
