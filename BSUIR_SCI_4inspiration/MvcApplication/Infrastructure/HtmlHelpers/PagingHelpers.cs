using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace MvcApplication.Infrastructure.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString ViewSetPageLinks(
          this HtmlHelper html,
          PagingInfo pagingInfo,
          int userID,
          int pictureSetID,
          int currentPicOrTagPage,
          Func<int, int, int, int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag_li = new TagBuilder("li"); // Construct an <a> tag
                TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(userID, pictureSetID, currentPicOrTagPage, i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                tag_li.InnerHtml = tag.ToString();
                result.Append(tag_li.ToString());           
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString ProfilePageLinks(
          this HtmlHelper html,
          PagingInfo pagingInfo,
          int userID,
          int currentSetOrTagPage,
          Func<int, int, int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag_li = new TagBuilder("li"); // Construct an <a> tag
                TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(userID, currentSetOrTagPage, i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                tag_li.InnerHtml = tag.ToString();
                result.Append(tag_li.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString MainPageLinks(
         this HtmlHelper html,
         PagingInfo pagingInfo,
         int userID,
         Func<int, int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag_li = new TagBuilder("li"); // Construct an <a> tag
                TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(userID, i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                tag_li.InnerHtml = tag.ToString();
                result.Append(tag_li.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

    }

}