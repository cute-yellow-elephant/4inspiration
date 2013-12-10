using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PictureSet:EntityBase
    {
        [StringLength(100)]
        public string Name { set; get; }
        public byte[] CoverData { get; set; }
        public string CoverMimeType { get; set; }
        public DateTime CreationDate { set; get; }
        public virtual ICollection<Picture> Pictures { set; get; }
        public int? ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        public PictureSet() { }

        public PictureSet(string name, DateTime creationDate, byte[] coverData, string coverMimeType, int? profileID)
        {
            Name = name;
            CreationDate = creationDate;
            CoverData = coverData;
            CoverMimeType = coverMimeType;
            ProfileId = profileID;
        }
    }
}
