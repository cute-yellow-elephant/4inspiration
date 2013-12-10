using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Common.Logging
{
    public class ProjectLogger:ILogger
    {
        private Logger _logger;
        public ProjectLogger(){ _logger = LogManager.GetCurrentClassLogger(); }
        public void WriteIfErrorOccured(string msg){ _logger.Error(msg); }
        public void WriteProgramWorkflow(string msg){ _logger.Info(msg); }
    }
}
