using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebFormsApplication;
using System.Data.Entity;
using AppCore;
using SqlRepository;

namespace WebFormsApplication
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            if (!Database.Exists("Server=JULIA-NB;Integrated Security=True;MultipleActiveResultSets=True;Initial Catalog=4inspDBWbFrms;AttachDBFilename=|DataDirectory|WF4insp.mdf;User Instance=True"))
            {
                var db = new DBInitializer();
                db.InitializeDatabase(new ProjectDBContext());
            }
    

            RegisterRoutes(RouteTable.Routes);          
        }

        void RegisterRoutes(RouteCollection routes)
        {
            RouteTable.Routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.MapPageRoute(
                "HomeRoute",
                "Home",
                "~/Default.aspx"
            );
            routes.MapPageRoute(
                "UploadPicRoute",
                "UploadPic",
                "~/UploadPic.aspx"
            );
            routes.MapPageRoute(
                "GalleryRoute",
                "Gallery/{page}",
                "~/Gallery.aspx"
            );
        }


        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
