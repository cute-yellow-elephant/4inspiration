<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Gallery.aspx.cs" Inherits="WebFormsApplication.Gallery" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Body4Rendering" runat="server"> 
      
    <form runat="server">
    <div class="container marketing">
        <asp:ListView ID="pictureList" runat="server" DataKeyNames="ID" GroupItemCount="4" ItemType="Domain.Picture" >
            <EmptyDataTemplate>      
                <table id="EmptyTable" runat="server">        
                    <tr>          
                        <td class="col-md-3">There are <b>0</b> pics in the gallery</td>        
                    </tr>     
                </table>  
            </EmptyDataTemplate>  
            <EmptyItemTemplate>     
                <td id="EmptyItem" runat="server" />  
            </EmptyItemTemplate>  
           <GroupTemplate>    
            <tr ID="itemPlaceholderContainer" runat="server">      
                <td ID="itemPlaceholder" runat="server"></td>    
            </tr>  
           </GroupTemplate>  
           <ItemTemplate>    
                <td id="Td2" runat="server" class="col-md-3">  
                        <div class="thumbnail">
                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# "ShowImageHandler.ashx?id=" + Eval("ID")%>' />
                            <div class="btn-group dropdown" id="profile_pictures">
                                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                                <strong><%#: Item.Name %></strong> <span class="caret"></span></button>
                                <ul class="dropdown-menu" role="menu">
                                    <li class="dropdown-header"><%#:Item.ShortInfo %></li>
                                    <li class="dropdown-header">Was added <%#: Item.CreationDate %></li>
                                    <li class="divider"></li>
                                    <li class="dropdown-header">Tags:</li>                               
                                        <asp:Repeater ID="tags" runat="server" ItemType="Domain.Tag" DataSource="<%# GetTags(Item.ID) %>">
                                            <ItemTemplate>
                                                <a href="#"><%#:Item.Name %></a>
                                            </ItemTemplate>
                                        </asp:Repeater>                        
                                </ul>
                            </div>
                        </div>   
                </td>  
            </ItemTemplate>  
            <LayoutTemplate>    
                <table id="LayoutTable" runat="server">      
                    <tr id="Tr1" runat="server">        
                        <td id="Td3" runat="server">          
                            <table ID="groupPlaceholderContainer" runat="server">            
                                <tr ID="groupPlaceholder" runat="server" class="row"></tr>          
                            </table>        
                         </td>      
                     </tr>      
                     <tr id="Tr2" runat="server"><td id="Td4" runat="server"></td></tr>    
                </table>  
            </LayoutTemplate>
        </asp:ListView>
    </div>

    <div class="container marketing" style="text-align:center;">
    <ul class="pagination pagination-sm" id="pages_pics">
        <asp:DataPager ID="DataPagerPics" runat="server" PagedControlID="pictureList" PageSize="12" OnPreRender="DataPagerPics_PreRender" >
           <Fields>
            <asp:NextPreviousPagerField ShowPreviousPageButton="true" ShowFirstPageButton="true" ShowNextPageButton="false"  PreviousPageText="Previous" FirstPageText="First" /> 
            <asp:NumericPagerField ButtonCount="30" />          
            <asp:NextPreviousPagerField NextPageText="Next" ShowNextPageButton="true" ShowPreviousPageButton="false" ShowLastPageButton="true" LastPageText="Last" />  
           </Fields>
        </asp:DataPager>
    </ul>
    </div>

        </form>
</asp:Content>
