<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="UploadPic.aspx.cs" Inherits="WebFormsApplication.UploadPic" %>

<asp:Content ID="UploadBody" ContentPlaceHolderID="Body4Rendering" runat="server">
<br />
<form runat="server" id="UploadForm" class = "form-signin" enctype="multipart/form-data">
    <div class="container">
        <h2 class="form-signin-heading">To add a new picture to our site is a good idea.</h2>
        <h3>Go for it) </h3>
        <br />
        <asp:ValidationSummary runat="server" DisplayMode="List" CssClass="validation-summary-errors"/>
        <asp:Label runat="server" visible="false" CssClass="field-validation-error" Text=" There is no image to upload!" ID="lblNoPic"/>
        <br />

        <%--Entering name--%>
        <div class="row">
            <div class="col-md-1">
                <asp:RequiredFieldValidator ID="RequiredName" ControlToValidate="tboxName" 
                   ErrorMessage="Please, enter the name." Text="*" runat="server" CssClass="field-validation-error"/>
                <asp:CustomValidator ID="NameLengthValidator" ControlToValidate="tboxName" OnServerValidate="NameLengthValidator_ServerValidate"
                   ErrorMessage="The number of symbols must be from 3 to 14!" Text="*" runat="server" CssClass="field-validation-error"/>
             </div>
            <div class="col-md-8">
                <div class="editor-label"><label>Name of the picture (min 3, max 14 symbols):</label></div>
                <asp:TextBox id="tboxName" runat="server" MaxLength="14"/>  
            </div> 
        </div>
        <%--Entering description--%>
        <div class="row">
            <div class="col-md-1">
                <asp:RequiredFieldValidator ID="RequiredFieldVShortInfo" ControlToValidate="tboxShortInfo" 
                   ErrorMessage="Please, enter several words as description." Text="*" runat="server" CssClass="field-validation-error"/>
                <asp:CustomValidator ID="ShortInfoLengthValidator" ControlToValidate="tboxShortInfo" OnServerValidate="ShortInfoLengthValidator_ServerValidate"
                   ErrorMessage="The number of symbols must be from 3 to 200!" Text="*" runat="server" CssClass="field-validation-error"/>
            </div>
            <div class="col-md-8">
                <div class="editor-label"><label>Description of the picture (min 3, max 200):</label></div>
                <asp:TextBox id="tboxShortInfo" runat="server" TextMode="MultiLine" MaxLength="200"/> 
            </div>
         </div>

        <%--Entering tags--%>
        <div class="row">
            <div class="col-md-1">
                <asp:RequiredFieldValidator ID="RequiredFieldTags" ControlToValidate="tboxTags" 
                   ErrorMessage="Please, enter at least one tag." Text="*" runat="server" CssClass="field-validation-error"/>
                <asp:CustomValidator ID="TagsLengthValidator" ControlToValidate="tboxTags" OnServerValidate="TagsLengthValidator_ServerValidate"
                   ErrorMessage="The number of symbols must be from 3 to 200!" Text="*" runat="server" CssClass="field-validation-error"/>
            </div>
            <div class="col-md-8">
        <div class="editor-label"><label>Enter at least one tag in here (mib 3 symbols in one tag, max 200 - in all. Separate them by ','):</label></div>
        <asp:TextBox id="tboxTags" runat="server" MaxLength="200" />
             </div>
         </div>  
 
        <%--Uploading the pic--%>
        <div><h3>Uploading the image: </h3></div>

         <div class="row">
             <div class="col-md-1">
                <asp:Label runat="server" visible="false" CssClass="field-validation-error" ID="lblNoPicA">*</asp:Label>
            </div>
             <div class="col-md-8">
                 <input id="File" type="file" runat="server" /> 
             </div>
         </div>

        <div class="row" runat="server" id="Filename">
            <div class="col-md-8 col-md-offset-1" runat="server"><label> Filename: <span id="fname" runat="server"/></label></div>
        </div>
        <div class="row" runat="server" id="Bytes">
            <div class="col-md-8 col-md-offset-1" runat="server"><label> ContentLength: <span id="clength" runat="server"/> bytes</label></div>
        </div>
        <br />

        <div class="row" runat="server" >
            <div id="Div1" class="col-md-8 col-md-offset-1" runat="server">
                <asp:Button id="submit" style="color:white" runat="server" class="btn btn-lg btn-primary" type="submit" text="Press in order to upload the picture"/><br/><br/>
            </div>
        </div>

    </div>
</form>

</asp:Content>
