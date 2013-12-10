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
    public class BaseEntityBC<T> where T:EntityBase
    {
        protected IRepository<T> _entityRepository;
        protected ILogger _logger;

        public BaseEntityBC(ILogger logger, ProjectDBContext dbcontext){ _logger = logger;}

        public T Read(int id)
        {
            try { return _entityRepository.Read(id); }
            catch (Exception error)
            {
                _logger.WriteIfErrorOccured(error.Message);
                return null;
            }
        }

        public ICollection<T> ReadAll()
        {
            try { return _entityRepository.ReadAll(); }
            catch (Exception error)
            {
                _logger.WriteIfErrorOccured(error.Message);
                return null;
            }
        }

        public T Find(string name)
        {
            try { return _entityRepository.Find(name); }
            catch (Exception error)
            {
                _logger.WriteIfErrorOccured(error.Message);
                return null;
            }
        }

        public T Find(int id)
        {
            try { return _entityRepository.Find(id); }
            catch (Exception error)
            {
                _logger.WriteIfErrorOccured(error.Message);
                return null;
            }
        }

        public T Find(int id, string name)
        {
            try { return _entityRepository.Find(id, name); }
            catch (Exception error)
            {
                _logger.WriteIfErrorOccured(error.Message);
                return null;
            }
        }

        public void Update(T ent)
        {
            try { _entityRepository.Update(ent); }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }

        public void Delete(int id)
        {
            try { _entityRepository.Delete(id); }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }
    }
}
