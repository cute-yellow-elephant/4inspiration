<%@ Page Title="Welcome!" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsApplication._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="Body4Rendering">
     <!-- Carousel
      ================================================== -->
      <div id="myCarousel" class="carousel slide">
        <!-- Indicators -->
        <ol class="carousel-indicators">
          <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
          <li data-target="#myCarousel" data-slide-to="1"></li>
        </ol>
        <div class="carousel-inner">
          <div class="item active">
            <asp:Image runat="server" ID="cloud" SkinID="FirstSlide"/>
            <div class="container">
              <div class="carousel-caption">
                <h1>Welcome to our Anonymous Gallery</h1>
                <p>At this modest tiny web-site we collect images of all genres, uploaded by You. Yes, You.
                    Attention: no registration is required! You can upload pics anonymously ^_^
                </p>                
              </div>
            </div>
          </div>
          <div class="item">
            <asp:Image ID="cat" runat="server" SkinID="SecondSlide" />
            <div class="container">
              <div class="carousel-caption">
                <h1> May be, You want to upload a pic right now?</h1>
                <p>  It's really easy! Just push the button below or select the appropriate menu item at the top of the page</p>
              </div>
            </div>
          </div>
        </div>
        <a class="left carousel-control" href="#myCarousel" data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a>
        <a class="right carousel-control" href="#myCarousel" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span></a>
      </div><!-- /.carousel -->

    <div class="main_buttons">
        <div class="row">
             <div class="col-md-3 col-md-offset-4">
                <p><a class="btn btn-large btn-primary" href="<%: GetRouteUrl("GalleryRoute", new { page = 1})%>">Search the gallery</a></p>
                <p><a class="btn btn-large btn-primary" href="<%: GetRouteUrl("UploadPicRoute", null) %>">Upload a new pic</a></p>
             </div>
         </div>
    </div>

</asp:Content>

