using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace SqlRepository.Repositories
{
    public class CountryRepository:RepositoryBasis<Country>
    {
        protected override DbSet<Country> _table { get { return _dbContext.Countries; } }
        public CountryRepository(ProjectDBContext dbContext) : base(dbContext) { }

        public override void Create(Country entity)
        {
            var en = this.Find(entity.Name);
            if (en != null)
                throw new Exception("The country " + en.Name + " with code " + en.Code.ToString() + " already exists in the database");  
            base.Create(entity);
        }

        public override Country Read(int id)
        {
            if (_table.Find(id) == null)
                return null;
            return base.Read(id);
        }

        public override void Update(Country entity)
        {
            var x = this.Find(entity.ID);
            x.Code = entity.Code;
            x.Name = entity.Name;
            _dbContext.SaveChanges();
        }

        public override Country Find(string name)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach (var x in list)
                if (String.Compare(x.Name, name) == 0)
                    return x;
            return null;
        }
    }
}
