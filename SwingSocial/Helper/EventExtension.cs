using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class EventExtension
    {
        public static Event AddExtraInfo(this Event ev)
        {
            Event eventOut = new Event();
                ev.ImagesString = ev.Images==null?"": String.Join(",", ev.Images);
                var htmlSource = new HtmlWebViewSource();
                StringBuilder htmlStr = new StringBuilder("");
                htmlStr.Append("<html style=\"background-color:black\"><body>");
                htmlStr.Append("<div class=\"mantine-68nn3q\" data-with-border=\"true\">");
                htmlStr.Append("<table class=\"mantine-8drano\"><tbody ><tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Start</div></td>");
                htmlStr.Append("<td ><div class=\"mantine-1as06hs\">" + ev.StartTime + "</div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">End</div></td>");
                htmlStr.Append("<td ><div class=\"mantine-1as06hs\">" + ev.EndTime + "</div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Venue</div></td>");
                htmlStr.Append("<td ><div class=\"mantine-1as06hs\">" + ev.Venue + "</div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Address</div></td>\r\n");
                htmlStr.Append("<td ><div><span>" + ev.Address + "</span></div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Type</div></td>");
                htmlStr.Append("<td ><div><span>" + ev.Category + "</span></div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Host</div></td>");
                htmlStr.Append("<td ><div><span>" + String.Empty + "</span></div></td></tr>");
                htmlStr.Append("</tbody></table></div>");
                htmlStr.Append("</body>");
                htmlStr.Append("<style>");
                htmlStr.Append("th,td {border: 0.0625rem solid rgb(55, 58, 64);border-collapse: collapse;border-spacing:20px;}");
                htmlStr.Append("td {padding-top:6px;padding-bottom:6px;padding-left:6px;}");
                htmlStr.Append(".mantine-8drano {font-family: Inter, sans-serif;width: 100%;border-collapse: collapse;caption-side: top;color: rgb(193, 194, 197);line-height: 1.55;}");
                htmlStr.Append(".mantine-68nn3q {outline: 0px;display: block;text-decoration: none;color: rgb(193, 194, 197);background-color: rgb(26, 27, 30);box-sizing: border-box;border-radius: 0.25rem;box-shadow: none;}");
                htmlStr.Append(".mantine-68nn3q[data-with-border] {border: 0.0625rem solid rgb(55, 58, 64);}");
                htmlStr.Append(".mantine-woghk7 {font-family: Inter, sans-serif;color: inherit;font-size: inherit;line-height: 1.55;text-decoration: none;font-weight: 500;}");
                htmlStr.Append(".mantine-1fx8ev9 {background-color: rgb(37, 38, 43);width: 140px;}");
                htmlStr.Append(".mantine-1as06hs {font-family: Inter, sans-serif;color: inherit;font-size: inherit;line-height: 1.55;text-decoration: none;}");
                htmlStr.Append("table {border-color: inherit;border-collapse: collapse;}");
                htmlStr.Append("</style>");
                htmlStr.Append("</html>");
                htmlSource.Html = htmlStr.ToString();
                ev.DetailsHtmlTable = htmlSource;
 
            return ev;
        }
    }
}
