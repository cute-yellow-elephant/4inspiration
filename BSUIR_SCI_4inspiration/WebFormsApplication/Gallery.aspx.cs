using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCore;
using Domain;
using System.Drawing;
using System.IO;

namespace WebFormsApplication
{
    public partial class Gallery : System.Web.UI.Page
    {
        private CoreHolder core;
        private const int pageSize = 12;

        protected void Page_Load(object sender, EventArgs e)
        {
            core = new CoreHolder();
        }

        public List<Picture> GetPictures()
        {
            var list = core.PictureRepository.ReadAll().OrderByDescending(x => x.ID).ToList();
            var temp_pic = core.PictureRepository.Read(1);
            if(list.Contains(temp_pic))
                list.Remove(temp_pic);
            return list;
        }

        public List<Tag> GetTags(int id)
        {
            return core.PictureRepository.Read(id).Tags.ToList(); 
        }

        protected void DataPagerPics_PreRender(object sender, EventArgs e)
        {
            this.pictureList.DataSource = this.GetPictures();
            this.pictureList.DataBind();
        }
    }
}