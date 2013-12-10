using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations.Resources;

namespace Domain
{
    public class Profile:EntityBase
    {
        public int UserID { set; get; }
        public virtual User User { set; get; }
        [StringLength(50)]
        public string Name { set; get; }
        [StringLength(200)]
        public string PersonalInfo { set; get; }
        public int? CountryID { set; get; }
        public virtual Country Country { set; get; }
        public int? LanguageID { set; get; }
        public virtual Language Language { set; get; }
        [StringLength(200)]
        public string AccountsLinks { set; get; }
        public bool WeeklyNewsletter { set; get; }

        public virtual ICollection<Picture> HeartedPics { set; get; }
        public virtual ICollection<Profile> Followers { set; get; }
        public virtual ICollection<Profile> Following { set; get; }
        public virtual ICollection<PictureSet> PictureSets { set; get; }

        public byte[] AvatarData { get; set; }
        public string AvatarMimeType { get; set; }

        public Profile() { }
        public Profile(int userID, string name, string personalInfo, int languageID, int countryID, string accountsLinks, 
            bool weeklyNewsletter, ICollection<Picture> heartedPics, ICollection<Profile> followers, ICollection<Profile> following,
            ICollection<PictureSet> pictureSets, ICollection<Tag> tags)
        {
            UserID = userID;
            Name = name;
            PersonalInfo = personalInfo;
            LanguageID = languageID;
            CountryID = countryID;
            AccountsLinks = accountsLinks;
            WeeklyNewsletter = weeklyNewsletter;
            HeartedPics = heartedPics;
            Followers = followers;
            Following = following;
            PictureSets = pictureSets;
        }

        public Profile(int userID, string name, string personalInfo, int languageID, int countryID, string accountsLinks,
           bool weeklyNewsletter, byte[] avatarData, string avatarMimeType)
        {
            UserID = userID;
            Name = name;
            PersonalInfo = personalInfo;
            LanguageID = languageID;
            CountryID = countryID;
            AccountsLinks = accountsLinks;
            WeeklyNewsletter = weeklyNewsletter;
            AvatarData = avatarData;
            AvatarMimeType = avatarMimeType;
        }
    }
}
