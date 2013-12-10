using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace SqlRepository.Repositories
{
    public class ProfileRepository : RepositoryBasis<Profile>
    {
        protected override DbSet<Profile> _table { get { return _dbContext.Profiles; } }
        public ProfileRepository(ProjectDBContext dbContext) : base(dbContext) { }

        public override void Create(Profile entity)
        {
            var en = this.Find(entity.UserID);
            if (en != null)
                throw new Exception("The profile " + en.Name + " already exists in the database");
            base.Create(entity);
        }

        public override Profile Read(int id)
        {
            if (_table.Find(id) == null)
                return null;
            return base.Read(id);
        }

        public override void Update(Profile entity)
        {
            var x = this.Find(entity.UserID);
            x.AccountsLinks = entity.AccountsLinks;
            x.Name = entity.Name;
            x.Country = entity.Country;
            x.CountryID = entity.CountryID;
            x.Followers = entity.Followers;
            x.Following = entity.Following;
            x.HeartedPics = entity.HeartedPics;
            x.WeeklyNewsletter = entity.WeeklyNewsletter;
            x.AvatarData = entity.AvatarData;
            x.AvatarMimeType = entity.AvatarMimeType;
            x.UserID = x.UserID;
            _dbContext.SaveChanges();
        }

        public override Profile Find(int id)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach (var x in list)
                if (x.UserID == id)
                    return x;
            return null;
        }
    }
}
