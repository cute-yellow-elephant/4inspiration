using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;

namespace SqlRepository.Repositories
{
    public abstract class RepositoryBasis<T> : IRepository<T> where T : EntityBase
    {
        protected ProjectDBContext _dbContext;
        protected abstract DbSet<T> _table { get; }
        public RepositoryBasis(ProjectDBContext dbContext){ _dbContext = dbContext; }
        public virtual void Create(T entity){ _table.Add(entity);}
        public virtual T Read(int id){ return _table.Find(id); }

        public ICollection<T> ReadAll()
        {
            if (_table.Count<T>() == 0)
                return null;
            return _table.ToList<T>();
        }

        public virtual void Update(T entity) { }
        public virtual void Delete(int id){ _table.Remove(this.Read(id)); }
        public virtual T Find(string name) { return null; }
        public virtual T Find(int id, string name) { return null; }
        public virtual T Find(int id) {return Read(id);}
    }
}