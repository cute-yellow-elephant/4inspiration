using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class Country:EntityBase
    {
        public string Name { set; get; }
        public int Code { set; get; }

        public Country() { }
        public Country(string name, int code)
        {
            Name = name;
            Code = code;
        }
    }
}
