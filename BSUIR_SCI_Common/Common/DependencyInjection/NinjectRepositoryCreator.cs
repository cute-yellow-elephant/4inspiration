using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Common.Logging;
using SqlRepository;
using Domain;
using SqlRepository.Repositories;

namespace Common.DependencyInjection
{
    public class NinjectRepositoryCreator : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRepository<User>>().To<UserRepository>().InSingletonScope();
            this.Bind<IRepository<Role>>().To<RoleRepository>().InSingletonScope();
            this.Bind<IRepository<Profile>>().To<ProfileRepository>().InSingletonScope();
            this.Bind<IRepository<Language>>().To<LanguageRepository>().InSingletonScope();
            this.Bind<IRepository<Country>>().To<CountryRepository>().InSingletonScope();
            this.Bind<IRepository<Picture>>().To<PictureRepository>().InSingletonScope();
            this.Bind<IRepository<PictureSet>>().To<PictureSetRepository>().InSingletonScope();
            this.Bind<IRepository<Tag>>().To<TagRepository>().InSingletonScope();
        }
    }
}
