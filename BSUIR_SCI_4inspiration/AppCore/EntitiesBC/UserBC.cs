using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Ninject;
using Common.Logging;
using SqlRepository;

namespace AppCore.EntitiesBC
{
    public class UserBC : BaseEntityBC<User>
    {
        public UserBC(ILogger logger, ProjectDBContext dbcontext) : base(logger, dbcontext) 
        {
            _entityRepository = new SqlRepository.Repositories.UserRepository(dbcontext);
        }

        public void Create(string login, string email, int password,
            DateTime addedDate, DateTime lastVisitDate)
        {
            try
            {
                _entityRepository.Create(new User(login,email,password,addedDate,lastVisitDate));
                var c = _entityRepository.Find(login);
                _logger.WriteProgramWorkflow(String.Format("New user was added: {0}, id={1}", c.Login, c.ID));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }

        public bool IsEmailRegistered(string email)
        {
            var list = this.ReadAll();
            foreach (var x in list)
                if (String.Compare(email, x.Email) == 0)
                    return true;
            return false;
        }

        public User FindByEmail(string email)
        {
            var list = this.ReadAll();
            foreach (var x in list)
                if (String.Compare(email, x.Email) == 0)
                    return x;
            return null;
        }       
    }
}
