using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;

namespace SqlRepository.Repositories
{
    public class UserRepository:RepositoryBasis<User>
    {
        protected override DbSet<User> _table { get { return _dbContext.Users; } }
        public UserRepository(ProjectDBContext dbContext) : base(dbContext) { }

        public override void Create(User entity)
        {
            var en = this.Find(entity.Login);
            if (en != null)
                throw new Exception("The user " + en.Login + " already exists in the database");
            base.Create(entity);
        }

        public override User Read(int id)
        {
            if (_table.Find(id) == null)
                return null;
            return base.Read(id);
        }

        public override void Update(User entity)
        {
            var x = this.Find(entity.Login);
            x.AddedDate = entity.AddedDate;
            x.Email = entity.Email;
            x.LastVisitDate = entity.LastVisitDate;
            x.Login = entity.Login;
            x.Password = entity.Password;
            x.Roles = entity.Roles;
            _dbContext.SaveChanges();
        }

        public override User Find(string name)
        {
            var list = this.ReadAll();
            if (list == null)
                return null;
            foreach(var x in list)
                if (String.Compare(x.Login, name) == 0)
                    return x;
            return null;
        }
    }
}
