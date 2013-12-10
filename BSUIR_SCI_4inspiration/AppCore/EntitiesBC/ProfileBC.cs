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
    public class ProfileBC : BaseEntityBC<Profile>
    {
        public ProfileBC(ILogger logger, ProjectDBContext dbcontext) : base(logger, dbcontext)
        {
            _entityRepository = new SqlRepository.Repositories.ProfileRepository(dbcontext);
        }

        public void Create(int userID, string name, string personalInfo, int languageID, int countryID, string accountsLinks,
            bool weeklyNewsletter, byte[] avatarData, string avatarMimeType)
        {
            try
            {
                _entityRepository.Create(new Profile(userID, name, personalInfo, languageID, countryID, accountsLinks, weeklyNewsletter, avatarData, avatarMimeType));
                var c = _entityRepository.Find(userID);
                _logger.WriteProgramWorkflow(String.Format("New profile was added: {0}, user id is {1}", c.Name, c.ID));
            }
            catch (Exception error) { _logger.WriteIfErrorOccured(error.Message); }
        }

        public int CountLikes(int userID){ return this.Find(userID).HeartedPics.Count();}
        public int CountPictureSets(int userID){ return this.Find(userID).PictureSets.Count(); }
        public int CountFollowers(int userID){ return this.Find(userID).Followers.Count(); }
        public int CountFollowing(int userID){ return this.Find(userID).Following.Count();}

        public List<Tag> GetTags(int userID)
        {
            var list = new List<Tag>();
            foreach (var set in this.Find(userID).PictureSets)
                foreach (var pic in set.Pictures)
                    foreach (var tag in pic.Tags)
                        if (list.Count() == 0)
                            list.Add(tag);
                        else if ( list.Contains(tag) == false)
                                list.Add(tag);
            return list;
        }
    }
}
