using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class EntityBase
    {
        [ScaffoldColumn(false)]
        public int ID { set; get; }

        public EntityBase() 
        {
            ID = Guid.NewGuid().GetHashCode();
        }
    }
}
