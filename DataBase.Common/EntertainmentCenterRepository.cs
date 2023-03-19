#nullable disable
using DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Common;

internal class EntertainmentCenterRepository : IRepository<EntertainmentCenter>
{
    public DBContext Context { get; init; }

    public EntertainmentCenterRepository(DBContext context) => Context = context;

    public void Create(EntertainmentCenter Item)
    {
        Context.Add(Item);
        Context.SaveChanges();
    }

    public void Delete(int id)
    {
        Context.Remove(GetItem(id));
        Context.SaveChanges();
    }

    public EntertainmentCenter GetItem(int id) => GetItems().SingleOrDefault(p => p.Id == id);

    public IEnumerable<EntertainmentCenter> GetItems()
    {
        return Context.EntertainmentCenters;
    }

    public void Update(EntertainmentCenter Item)
    {
        Context.Update(Item);
        Context.SaveChanges();
    }
}
