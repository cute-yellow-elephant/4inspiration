using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace SqlRepository.Repositories
{
    public class TagRepository : RepositoryBasis<Tag>
    {
        protected override DbSet<Tag> _table { get { return _dbContext.Tags; } }
        public TagRepository(ProjectDBContext dbContext) : base(dbContext) { }

        public override void Create(Tag entity)
        {
            var en = this.Find(entity.Name);
            if (en != null)
                throw new Exception(String.Format("The tag {0} already exists in the database", en.Name));
            base.Create(entity);
        }

        public override Tag Read(int id)
        {
            if (_table.Find(id) == null)
                return null;
            return base.Read(id);
        }

        public override void Update(Tag entity)
        {
            var x = this.Read(entity.ID);
            x.Pictures = entity.Pictures;
            x.Name = entity.Name;
        }

        public override Tag Find(string name)
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
