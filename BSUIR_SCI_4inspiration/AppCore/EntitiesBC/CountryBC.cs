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
    public class CountryBC:BaseEntityBC<Country>
    {
        public CountryBC(ILogger logger, ProjectDBContext dbcontext) : base(logger, dbcontext) 
        {
            _entityRepository = new SqlRepository.Repositories.CountryRepository(dbcontext);
        }

        public void Create(string name, int code)
        {
            try
            {
                _entityRepository.Create(new Country(name, code));
                var c = _entityRepository.Find(name);
                _logger.WriteProgramWorkflow(String.Format("New country was added: {0}, {1}",c.Name, c.Code.ToString()));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }

    }
}
