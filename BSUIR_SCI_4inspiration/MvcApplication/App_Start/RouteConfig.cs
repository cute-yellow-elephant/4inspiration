using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("AddHeartedPic", "Heart_Like/pic{pictureID}/user{userID}", new { controller = "Home", action = "AddToHeartedPics" });
            routes.MapRoute("DeleteHeartedPic", "Dislike/pic{pictureID}/user{userID}", new { controller = "Home", action = "DeleteFromHeartedPics" });
            routes.MapRoute("Index", "Main/{userID}/", new { controller = "Home", action = "Index" });
            routes.MapRoute("RegisterUser", "Registration/{*catchall}", new { controller = "Account", action = "Register" });
            routes.MapRoute("ProfileCreation", "Profile_Creation/{*catchall}", new { controller = "Account", action = "CreateProfile" });
            routes.MapRoute("ProfilePage", "Profile_Page/user{userID}/Options/{*catchall}", new { controller = "Account", action = "ProfilePage" });
            routes.MapRoute("ProfilePageLikes", "Profile_Page/user{userID}/Likes/{*catchall}", new { controller = "Account", action = "ProfilePageLikes" });
            routes.MapRoute("ProfilePageSets", "Profile_Page/user{userID}/Sets/{*catchall}", new { controller = "Account", action = "ProfilePageSets" });
            routes.MapRoute("SignIn", "Sign_in/{*catchall}", new { controller = "Account", action = "SignIn" });
            routes.MapRoute("AddPictureSet", "Profile_Page/{userID}/Add_Picture_Set/", new { controller = "PictureSet", action = "AddPictureSet"});
            routes.MapRoute("ViewPictureSet", "Profile_Page/View_Picture_Set/user{userID}/set{pictureSetID}/", new { controller = "Account", action = "ProfilePageViewSet" });
            routes.MapRoute("AddPicture", "Profile_Page/{userID}/set{pictureSetID}/Add_Picture/", new { controller = "Picture", action = "AddPicture"});
            routes.MapRoute("ManageTags", "Manage_Tags/", new { controller = "Home", action = "ManageTags" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            
         }
    }
}