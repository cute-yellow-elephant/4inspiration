using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Picture:EntityBase
    {
        [Required(ErrorMessage="Please, enter the name")]
        [StringLength(14, MinimumLength = 3, ErrorMessage = "Please enter a valid name of the picture")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Please, enter several words about the pic"), DataType(DataType.MultilineText)]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Please enter a valid description of the picture")]
        public string ShortInfo { set; get; }
        public byte[] PictureData { get; set; }
        public string PictureMimeType { get; set; }

        public virtual ICollection<Tag> Tags { set; get; }
        public virtual ICollection<Profile> PeopleWhoLiked { set; get; }
        public int? PictureSetId { get; set; }//foreign key for picture set in profile
        public virtual PictureSet PictureSet { get; set; }
        public DateTime CreationDate { set; get; }

        public Picture() { }
        public Picture(string name, string shortInfo, byte[] pictureData, string pictureMimeType, int? pictureSetID, DateTime creationDate) 
        {
            Name = name;
            ShortInfo = shortInfo;
            PictureData = pictureData;
            PictureMimeType = pictureMimeType;
            PictureSetId = pictureSetID;
            CreationDate = creationDate;
        }
    }
}
