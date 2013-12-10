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
    public class RoleBC : BaseEntityBC<Role>
    {
        public RoleBC(ILogger logger, ProjectDBContext dbcontext) : base(logger, dbcontext) 
        {
            _entityRepository = new SqlRepository.Repositories.RoleRepository(dbcontext);
        }

        public void Create(string name)
        {
            try
            {
                _entityRepository.Create(new Role(name));
                _logger.WriteProgramWorkflow(String.Format("New role was added: {0}", name));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }
    }
}
