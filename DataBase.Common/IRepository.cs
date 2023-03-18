using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Common
{
    public interface IRepository<T> where T : class
    {
        
        IEnumerable<T> GetItems();

        T GetItem(int id);

        void Create(T Item);

        void Update(T Item);

        void Delete(int id);
    }
}
