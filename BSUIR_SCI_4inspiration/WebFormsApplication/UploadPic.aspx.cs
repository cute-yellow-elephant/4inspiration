using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCore;
using Domain;
using System.Text.RegularExpressions;

namespace WebFormsApplication
{
    public partial class UploadPic : System.Web.UI.Page
    {
        private CoreHolder core;
        private Picture pic;

        protected void Page_Load(object sender, EventArgs e)
        {
            core = new CoreHolder(); 
            var temp_pic = core.PictureRepository.Read(1);
            if (!IsPostBack)
            {
                temp_pic.PictureData = null;
                core.Submit();
            }
            if (IsPostBack)
            {           
                if (File.PostedFile.ContentLength != 0)
                {
                    fname.InnerHtml = File.PostedFile.FileName;
                    clength.InnerHtml = File.PostedFile.ContentLength.ToString();
                    lblNoPic.Visible = false;
                    lblNoPicA.Visible = false;
                    temp_pic.PictureData = new byte[File.PostedFile.ContentLength];
                    File.PostedFile.InputStream.Read(temp_pic.PictureData, 0, File.PostedFile.ContentLength);
                    temp_pic.PictureMimeType = File.PostedFile.ContentType;
                    core.Submit();
                }
                else if (temp_pic.PictureData == null)
                    {
                        lblNoPic.Visible = true;
                        lblNoPicA.Visible = true;
                    }
                if ((!string.IsNullOrWhiteSpace(tboxName.Text)) && (!string.IsNullOrWhiteSpace(tboxShortInfo.Text))
                    && (!string.IsNullOrWhiteSpace(tboxTags.Text)) && (temp_pic.PictureData != null))
                {
                    core.PictureRepository.CreatePicture(tboxName.Text, tboxShortInfo.Text, temp_pic.PictureData, temp_pic.PictureMimeType, core.PictureSetRepository.Read(1).ID, DateTime.UtcNow);
                    pic = core.PictureRepository.Find(core.PictureSetRepository.Read(1).ID, tboxName.Text);
                    temp_pic.PictureData = null;
                    core.Submit();

                    string pattern = "(, )|(,)|( , )";
                    string[] tags = Regex.Split(tboxTags.Text, pattern, RegexOptions.IgnoreCase);
                    foreach (string match in tags)
                        if ((String.Compare(match, ",") != 0) && (String.Compare(match, ", ") != 0) && (String.Compare(match, " , ") != 0))
                        {
                            core.TagRepository.Create(match);
                            var tag = core.TagRepository.Find(match);
                            tag.Pictures = new List<Picture>();
                            tag.Pictures.Add(pic);
                            core.Submit();
                        }
                    Response.RedirectToRoute("GalleryRoute", new { page = 1});
                }
            }
        }

        protected void NameLengthValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var str = (String) args.Value;
            if ((str.Length < 3) || (str.Length > 14))
                args.IsValid = false;
            else args.IsValid = true;
        }

        protected void ShortInfoLengthValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var str = (String)args.Value;
            if ((str.Length < 3) || (str.Length > 200))
                args.IsValid = false;
            else args.IsValid = true;
        }

        protected void TagsLengthValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var str = (String)args.Value;
            if ((str.Length < 3) || (str.Length > 200))
                args.IsValid = false;
            else args.IsValid = true;
        }

        protected void NoFileValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (File.PostedFile == null) args.IsValid = false;
            else args.IsValid = true;
        }


    }
}