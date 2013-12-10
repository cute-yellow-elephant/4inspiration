using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public interface ILogger
    {
        void WriteIfErrorOccured(string msg);
        void WriteProgramWorkflow(string msg);
    }
}
