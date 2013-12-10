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
    public class TagBC: BaseEntityBC<Tag>
    {
        public TagBC(ILogger logger, ProjectDBContext dbcontext) : base(logger, dbcontext) 
        {
            _entityRepository = new SqlRepository.Repositories.TagRepository(dbcontext);
        }

        public void Create(string name)
        {
            try
            {
                _entityRepository.Create(new Tag(name));
                _logger.WriteProgramWorkflow(String.Format("New tag was added: {0}", name));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); throw new Exception(error.Message); }
        }

    }
}
