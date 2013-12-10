using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<T> where T:EntityBase
    {
        void Create(T entity);
        T Read(int id);
        ICollection<T> ReadAll();
        void Update(T entity);
        void Delete(int id);
        T Find(string name);
        T Find(int id);
        T Find(int id, string name);
    }
}
