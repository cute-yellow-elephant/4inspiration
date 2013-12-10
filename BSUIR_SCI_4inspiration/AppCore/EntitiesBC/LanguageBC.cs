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
    public class LanguageBC : BaseEntityBC<Language>
    {
        public LanguageBC(ILogger logger, ProjectDBContext dbcontext) : base(logger, dbcontext) 
        {
            _entityRepository = new SqlRepository.Repositories.LanguageRepository(dbcontext);
        }

         public void Create(string name, int code, string ISO6392)
         {
             try
             {
                 _entityRepository.Create(new Language(name, code, ISO6392));
                 var c = _entityRepository.Find(name);
                 _logger.WriteProgramWorkflow(String.Format("New language was added:{0}, {1}, {2}", c.Name, c.Code.ToString(), c.ISO639_2));
             }
             catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
         }
    }
}
