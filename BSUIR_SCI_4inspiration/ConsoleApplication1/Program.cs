using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using AppCore;
using SqlRepository;
using SqlRepository.Repositories;
using System.Data.Entity;
using Domain;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var kernel = new StandardKernel(new NinjectTableHolderCreator());
            //var obj = kernel.Get<DbHolder>();
            //obj.PictureHolder.CreateTableHolder();
            //Console.ReadLine();

            //using (var db = new ProjectDBContext())
            //{
            //    db.Pictures.Add(new Picture
            //    {
            //        Name = "Beautiful Flower",
            //        ShortInfo = "found it in my granny's garden",
            //        PicturePath = "fotoradical/blabla",
            //        Tags = new List<Tag>() { new Tag("nature"), new Tag("flowers") }
            //    });

            //    db.SaveChanges();
            //    var pic = db.Pictures.First();
            //    Console.WriteLine(pic.Name);
            //    pic.Name = "ugly flower";
            //    Console.WriteLine(pic.Name);

            //    var propertyNames =
            //      db.Entry(pic).CurrentValues.PropertyNames;
            //    foreach (var property in propertyNames)
            //    {
            //        System.Console.WriteLine(
            //          "{0}\n Original Value: {1}\n Current Value: {2}",
            //          property,
            //          db.Entry(pic).OriginalValues[property],
            //          db.Entry(pic).CurrentValues[property]);
            //    }
            //}


            //var sqlHolder = new SqlHolder();
            //var country_rep = new CountryRepository(sqlHolder.DBContext);
            //country_rep.Create(new Country("Belarus", 112));
            //country_rep.Create(new Country ("Belgium", 056));
            //foreach (var c in country_rep.ReadAll())

            var core = new CoreHolder();
            core.CountryRepository.Create("Belarus", 112);
            core.CountryRepository.Create("Spain", 134);
            core.CountryRepository.Create("Germany", 192);
            core.LanguageRepository.Create("Russian", 13, "ru");
            core.LanguageRepository.Create("English", 26, "en");
            core.LanguageRepository.Create("Bulgarian", 43, "bg");

            core.RoleRepository.Create("Admin");
            core.RoleRepository.Create("User");
            //core.UserRepository.Create("mimimi", "mimi@ya.ru", "3494333321".GetHashCode(),"ghg", DateTime.UtcNow, DateTime.UtcNow);

            var x = core.CountryRepository.ReadAll();
            foreach (var f in x)
                Console.WriteLine("Name of the country - {0}, code - {1}", f.Name, f.Code);
                
            Console.ReadKey();

            var z = core.LanguageRepository.ReadAll();
            foreach (var f in z)
                Console.WriteLine("Name of the lang - {0}, code - {1}", f.Name, f.Code);

            var y = core.RoleRepository.ReadAll();
            foreach (var f in y)
                Console.WriteLine("Name of the role - {0}, code - {1}", f.Name, f.ID);

            Console.ReadKey();

            //var z = core.UserRepository.ReadAll();
            //foreach (var f in z)
            //    Console.WriteLine("Name of the user - {0}, email - {1},{2}", f.Login, f.Email, f.AddedDate);

            //Console.ReadKey();


        }
    }
}
