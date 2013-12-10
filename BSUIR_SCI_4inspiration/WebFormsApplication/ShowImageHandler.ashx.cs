using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using AppCore;

namespace WebFormsApplication
{
    /// <summary>
    /// Summary description for ShowImageHandler
    /// </summary>
    public class ShowImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //creating object, working with db
            var core = new CoreHolder();
            //finding the picture by id - it works, the picture is found
            var id = Convert.ToInt32(context.Request.QueryString["id"]);
            var picture = core.PictureRepository.Read(id);
            //setting string with type 
            byte[] buffer = picture.PictureData;
            context.Response.ContentType = picture.PictureMimeType;           
            //trying to write byte[]
            context.Response.BinaryWrite(buffer);
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}