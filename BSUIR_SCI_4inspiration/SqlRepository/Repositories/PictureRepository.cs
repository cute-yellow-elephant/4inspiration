using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace SqlRepository.Repositories
{
    public class PictureRepository : RepositoryBasis<Picture>
    {
        protected override DbSet<Picture> _table { get { return _dbContext.Pictures; } }
        public PictureRepository(ProjectDBContext dbContext) : base(dbContext) { }

        public override Picture Read(int id)
        {
            if (_table.Find(id) == null)
                return null;
            return base.Read(id);
        }

        public override void Update(Picture entity)
        {
            var x = this.Find(entity.ID);
            x.PictureData = entity.PictureData;
            x.PictureMimeType = entity.PictureMimeType;
            x.Name = entity.Name;
            x.ShortInfo = entity.ShortInfo;
            x.Tags = entity.Tags;
            _dbContext.SaveChanges();
        }

        public override Picture Find(int id, string name)
        {
            if (this.ReadAll() == null)
                return null;
            var obj = this.ReadAll()
                .Where(x => x.PictureSetId == id)
                .Where(y => y.Name == name).FirstOrDefault();
            return obj;
        }
    }
}
