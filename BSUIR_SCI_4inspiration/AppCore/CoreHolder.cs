using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Common.DependencyInjection;
using Domain;
using SqlRepository;
using SqlRepository.Repositories;
using System.Data.Entity;
using Ninject;
using AppCore.EntitiesBC;

namespace AppCore
{
    public class CoreHolder
    {
        private ILogger _logger;
        private ProjectDBContext _dbcontext;
        public UserBC UserRepository { get; set; }
        public CountryBC CountryRepository { get; set; }
        public LanguageBC LanguageRepository { get; set; }
        public PictureBC PictureRepository { get; set; }
        public PictureSetBC PictureSetRepository { get; set; }
        public RoleBC RoleRepository { get; set; }
        public ProfileBC ProfileRepository { get; set; }
        public TagBC TagRepository { get; set; }

        public void Submit()
        {
            _dbcontext.SaveChanges();
        }

        private void InitializeRepositories()
        {
            UserRepository = new UserBC(_logger, _dbcontext);
            CountryRepository = new CountryBC( _logger, _dbcontext);
            LanguageRepository = new LanguageBC( _logger, _dbcontext);
            PictureRepository = new PictureBC(_logger, _dbcontext);
            PictureSetRepository = new PictureSetBC( _logger, _dbcontext);
            RoleRepository = new RoleBC( _logger, _dbcontext);
            ProfileRepository = new ProfileBC(_logger, _dbcontext);
            TagRepository = new TagBC( _logger, _dbcontext);
        }

        public CoreHolder()
        {
            var serviceLocator = new StandardKernel(new NinjectLoggerCreator());
            _logger = serviceLocator.Get<ILogger>();
            _dbcontext = new ProjectDBContext();
            InitializeRepositories();
        }

    }
}
