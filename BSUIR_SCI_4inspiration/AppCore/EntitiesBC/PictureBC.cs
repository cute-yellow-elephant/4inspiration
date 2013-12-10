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
    public class PictureBC: BaseEntityBC<Picture>
    {
        public PictureBC(ILogger logger, ProjectDBContext dbcontext) : base(logger, dbcontext) 
        {
            _entityRepository = new SqlRepository.Repositories.PictureRepository(dbcontext);
        }

        public void CreatePicture(string name, string shortInfo, byte[] pictureData, string pictureMimeType, int? pictureSetID, DateTime creationDate)
        {
            try
            {
                _entityRepository.Create(new Picture(name, shortInfo, pictureData, pictureMimeType, pictureSetID, creationDate));
                var c = _entityRepository.Find((int) pictureSetID);
                _logger.WriteProgramWorkflow(String.Format("New picture was added: {0}, {1}, {2})", c.Name, c.ID, c.CreationDate));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }

        public void AddTag(int pictureID, string name)
        {
            var pic = this.Find(pictureID);
            if (pic.Tags.Where(x => x.Name == name).First() == null)
                pic.Tags.Add(new Tag(name));
        }

        public List<Tag> GetTags(int pictureID)
        {
            try
            {
                return this.Read(pictureID).Tags.ToList();
            }
            catch { return null; }
        }
    }
}
