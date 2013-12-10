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
    public class PictureSetBC : BaseEntityBC<PictureSet>
    {
        public PictureSetBC(ILogger logger, ProjectDBContext dbcontext) : base(logger, dbcontext) 
        {
            _entityRepository = new SqlRepository.Repositories.PictureSetRepository(dbcontext);
        }

         public void Create(string name, DateTime creationDate, byte[] pictureData, string pictureMimeType, int? profileID)
         {
             try
             {
                 _entityRepository.Create(new PictureSet(name, creationDate, pictureData, pictureMimeType, profileID));
                 var c = _entityRepository.Find((int)profileID);
                 _logger.WriteProgramWorkflow(String.Format("New PictureSet was added: {0}, coverpath - {1}", c.Name, c.ID));
             }
             catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
         }

         public List<Tag> GetTags(int picSetID)
         {
             var list = new List<Tag>();
                 foreach (var pic in this._entityRepository.Read(picSetID).Pictures)
                     foreach (var tag in pic.Tags)
                         if (list.Count() == 0)
                             list.Add(tag);
                         else if (list.Contains(tag) == false)
                             list.Add(tag);
             return list;
         }
    }
}
