using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Language:EntityBase
    {
        public string Name { set; get; }
        public int Code { set; get; }
        public string ISO639_2 { set; get; }

        public Language() { }
        public Language(string name, int code, string ISO6392)
        {
            Name = name;
            Code = code;
            ISO639_2 = ISO6392;
        }
    }
}
