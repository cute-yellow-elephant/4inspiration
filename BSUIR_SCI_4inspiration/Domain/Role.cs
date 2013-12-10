using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Role: EntityBase
    {
        [StringLength(20)]
        public string Name { set; get; }
        public virtual ICollection<User> Users { set; get; }

        public Role() { }
        public Role(string name){Name = name;}
    }
}
