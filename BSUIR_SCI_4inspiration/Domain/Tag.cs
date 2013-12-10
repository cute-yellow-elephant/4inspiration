using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Domain
{
    public class Tag:EntityBase
    {
        [Required, StringLength(50)]
        public string Name { set; get; }
        public virtual ICollection<Picture> Pictures { set; get; }

        public Tag() { }
        public Tag(string name)
        {
            Name = name;
        }
    }
}
