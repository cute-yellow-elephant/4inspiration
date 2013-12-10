using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SqlRepository;
using Domain;

namespace AppCore
{
    public class DBInitializer: DropCreateDatabaseAlways<ProjectDBContext>
    {
        protected override void Seed(ProjectDBContext context)
        {
            GetCountries().ForEach(c => context.Countries.Add(c));
            GetLanguages().ForEach(c => context.Languages.Add(c));
            GetRoles().ForEach(p => context.Roles.Add(p));
            context.Users.Add(new User("test","aa@mail.ru", 23232, DateTime.UtcNow, DateTime.UtcNow));
            context.SaveChanges();
            var user = context.Users.FirstOrDefault();
            var lang = context.Languages.FirstOrDefault();
            var count = context.Countries.FirstOrDefault();
            context.Profiles.Add(new Profile(user.ID, "john doe", "test", lang.ID, count.ID, "twitter", true, null, "png"));
            context.SaveChanges();
            context.PictureSets.Add(new PictureSet("test",DateTime.UtcNow, null, "png", context.Profiles.FirstOrDefault().ID));
            context.SaveChanges();
            context.Pictures.Add(new Picture("temp", "temp", null, "none", context.PictureSets.FirstOrDefault().ID, DateTime.UtcNow));
            context.SaveChanges();
        }

        private static List<Country> GetCountries()
        {
            var categories = new List<Country> 
            {
                new Country
                {
                    Name = "Belarus",
                    Code = 112,
                },
                new Country
                {
                    Name = "Russia",
                    Code = 145,
                },
                new Country
                {
                    Name = "England",
                    Code = 325,
                }
            };
            return categories;
        }

        private static List<Role> GetRoles()
        {
            var roles = new List<Role> 
            {
                new Role{ Name = "Admin",},
                new Role{ Name = "User",},
            };
            return roles;
        }

        private static List<Language> GetLanguages()
        {
            var languages = new List<Language> 
            {
                new Language
                {
                    Name="Russian",
                    ISO639_2 = "55",
                },
                new Language
                {
                   Name="English",
                   ISO639_2 = "65", 
                },
            };
            return languages;
        }        
    }
}
