using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace SqlRepository.Repositories
{
    public class PictureSetRepository: RepositoryBasis<PictureSet>
    {
        protected override DbSet<PictureSet> _table { get { return _dbContext.PictureSets; } }
        public PictureSetRepository(ProjectDBContext dbContext) : base(dbContext) { }

        public override PictureSet Read(int id)
        {
            if (_table.Find(id) == null)
                return null;
            return base.Read(id);
        }

        public override void Update(PictureSet entity)
        {
            var x = Find((int)entity.ProfileId);
            x.CoverData = entity.CoverData;
            x.CoverMimeType = entity.CoverMimeType;
            x.Name = entity.Name;
            x.CreationDate= entity.CreationDate;
            x.Pictures = entity.Pictures;
            _dbContext.SaveChanges();
        }

        public override PictureSet Find(int id)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach (var x in list)
                if (x.ProfileId == id)
                    return x;
            return null;
        }
    }
}
