using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace SqlRepository.Repositories
{
    public class LanguageRepository:RepositoryBasis<Language>
    {
        protected override DbSet<Language> _table { get { return _dbContext.Languages; } }
        public LanguageRepository(ProjectDBContext dbContext) : base(dbContext) { }

        public override void Create(Language entity)
        {
            var en = this.Find(entity.Name);
            if (en != null)
                throw new Exception("The language " + en.Name + " with code " + en.Code.ToString() + " already exists in the database");
            base.Create(entity);
        }

        public override Language Read(int id)
        {
            if (_table.Find(id) == null)
                return null;
                //throw new Exception("The language with id " + id.ToString() + " doesn't exist in the database");
            return base.Read(id);
        }

        public override void Update(Language entity)
        {
            var x = this.Find(entity.ID);
            x.Code = entity.Code;
            x.Name = entity.Name;
            x.ISO639_2 = entity.ISO639_2;
            _dbContext.SaveChanges();
        }

        public override Language Find(string name)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach (var x in list)
                if (String.Compare(x.Name, name) == 0)
                    return x;
            //throw new Exception("There is no language named " + name + " in the database");
            return null;
        }
    }
}
