using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Common.Logging;

namespace Common.DependencyInjection
{
    public class NinjectLoggerCreator : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ILogger>().To<ProjectLogger>();
        }
    }
}
