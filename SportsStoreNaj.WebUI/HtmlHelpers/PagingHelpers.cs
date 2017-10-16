using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using SportsStoreNaj.WebUI.Models;
namespace SportsStoreNaj.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <=pagingInfo.TotalPages; i++)
            {
                TagBuilder tagBuilder = new TagBuilder("a");
                tagBuilder.MergeAttribute("href", pageUrl(i));

                tagBuilder.InnerHtml = i.ToString();

                if (i==pagingInfo.CurrentPage)
                {
                    tagBuilder.AddCssClass("selected");
                    tagBuilder.AddCssClass("btn-primary");
                }
                tagBuilder.AddCssClass("btn btn-default");
                sb.Append(tagBuilder.ToString());
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}