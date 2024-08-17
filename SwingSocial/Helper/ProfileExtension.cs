using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class ProfileExtension
    {
        public static ProfileEntity AddExtraInfo(this ProfileEntity profile)
        {
            ProfileEntity myProfileOut = new ProfileEntity();

            try
            {
                if (profile.Avatar == null || profile.Avatar == "" || profile.Avatar.Contains("data:image/png"))
                {
                    profile.Avatar = "noavatar";
                }
                profile.BirthGender = profile.Gender == null ? String.Empty : profile.BirthGender.GetBirthGender(profile);
                profile.PartnerBirthGender = profile.PartnerGender == null ? String.Empty : profile.PartnerBirthGender.PartnerGetBirthGender(profile);
                profile.PartnerAgeInt = profile.PartnerAge == null ? 0 : Convert.ToInt32(profile.PartnerAge);
                profile.PartnerAge = Convert.ToInt32(profile.PartnerBirthGender.PartnerGetAge(profile.PartnerDateOfBirth));

                profile.AgeInt = profile.Age == null ? 0 : Convert.ToInt32(profile.Age);
                profile.Age = Convert.ToInt32(profile.BirthGender.GetAge(profile.DateOfBirth));
                profile.HeightInt = profile.Height == null ? 0 : Convert.ToInt32(profile.Height);
                profile.HeightString = profile.Height == null ? null : profile.BirthGender.GetHeightString(profile.HeightInt);
                profile.IdString = profile.BirthGender.GetIdString(profile.Id);
                profile.Location = profile.Location == null ? profile.Location : profile.Location.Length > 21 ? profile.Location.Substring(0, 21) : profile.Location;
                profile.LocationLength = profile.Location == null ? 0 : Convert.ToInt32(profile.Location.Length) * 10;
                profile.AccountTypeLength = profile.AccountType == null ? 0 : Convert.ToInt32(profile.AccountType.Length) * 10;
                profile.TagLineIsVisible = profile.Tagline == null ? false : profile.Tagline.Length == 0 ? false : true;
                profile.SwingStyleTagsObjectList = new List<SwingStyleTagsObject>();
                profile.ShowPartnerDetailsTitle = profile.AccountType=="Couple"?true:false;
                profile.ShowPartnerDetailsHtmlTable = false;
                if (profile.SwingStyleTags != null)
                {
                    foreach (var item in profile.SwingStyleTags)
                    {
                        SwingStyleTagsObject o = new SwingStyleTagsObject();
                        o.Description = item;
                        o.Length = Convert.ToInt32(item.Length) * 10;
                        profile.SwingStyleTagsObjectList.Add(o);
                    }
                }
                profile.SwingStyleTagsObjectListIsVisible = profile.SwingStyleTagsObjectList.Count == 0 ? false : true;
                profile.AboutIsVisible = profile.About == null ? false : profile.About.Length == 0 ? false : true;
                //profile.BirthGender = "34M | 33F";

                var htmlSource = new HtmlWebViewSource();
                StringBuilder htmlStr = new StringBuilder("");
                htmlStr.Append("<html style=\"background-color:black\"><body>");
                htmlStr.Append("<div class=\"mantine-68nn3q\" data-with-border=\"true\">");
                htmlStr.Append("<table class=\"mantine-8drano\"><tbody ><tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Age</div></td>");
                htmlStr.Append("<td ><div class=\"mantine-1as06hs\">" + profile.Age + "</div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Gender</div></td>");
                htmlStr.Append("<td ><div class=\"mantine-1as06hs\">" + profile.Gender + "</div></td></tr>");
                //htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Height</div></td>");
                //htmlStr.Append("<td ><div class=\"mantine-1as06hs\">" + profile.HeightString + "</div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Orientation</div></td>\r\n");
                htmlStr.Append("<td ><div><span>" + profile.SexualOrientation + "</span></div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Body Type</div></td>");
                htmlStr.Append("<td ><div><span>" + profile.BodyType + "</span></div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Eyes</div></td>");
                htmlStr.Append("<td ><div><span>" + profile.EyeColor + "</span></div></td></tr>");
                htmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Hair</div></td>");
                htmlStr.Append("<td ><div><span>" + profile.HairColor + "</span></div></td></tr>");
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
                profile.DetailsHtmlTable = htmlSource;
                //browser.Source = htmlSource;
                //Content = browser;
                if (profile.PartnerGender != null && profile.PartnerGender != string.Empty)
                {
                    profile.IsGenderDetailsHtmlTableVisible = true;
                    var partnerHtmlSource = new HtmlWebViewSource();
                    StringBuilder partnerHtmlStr = new StringBuilder("");
                    partnerHtmlStr.Append("<html style=\"background-color:black\"><body>");
                    partnerHtmlStr.Append("<div class=\"mantine-68nn3q\" data-with-border=\"true\">");
                    partnerHtmlStr.Append("<table class=\"mantine-8drano\"><tbody ><tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Age</div></td>");
                    partnerHtmlStr.Append("<td ><div class=\"mantine-1as06hs\">" + profile.PartnerAge + "</div></td></tr>");
                    partnerHtmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Gender</div></td>");
                    partnerHtmlStr.Append("<td ><div class=\"mantine-1as06hs\">" + profile.PartnerGender + "</div></td></tr>");
                    //partnerHtmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Height</div></td>");
                    //partnerHtmlStr.Append("<td ><div class=\"mantine-1as06hs\">" + profile.PartnerHeightString + "</div></td></tr>");
                    partnerHtmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Orientation</div></td>\r\n");
                    partnerHtmlStr.Append("<td ><div><span>" + profile.PartnerSexualOrientation + "</span></div></td></tr>");
                    partnerHtmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Body Type</div></td>");
                    partnerHtmlStr.Append("<td ><div><span>" + profile.PartnerBodyType + "</span></div></td></tr>");
                    partnerHtmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Eyes</div></td>");
                    partnerHtmlStr.Append("<td ><div><span>" + profile.PartnerEyeColor + "</span></div></td></tr>");
                    partnerHtmlStr.Append("<tr ><td class=\"mantine-1fx8ev9\"><div class=\"mantine-woghk7\">Hair</div></td>");
                    partnerHtmlStr.Append("<td ><div><span>" + profile.PartnerHairColor + "</span></div></td></tr>");
                    partnerHtmlStr.Append("</tbody></table></div>");
                    partnerHtmlStr.Append("</body>");
                    partnerHtmlStr.Append("<style>");
                    partnerHtmlStr.Append("th,td {border: 0.0625rem solid rgb(55, 58, 64);border-collapse: collapse;border-spacing:20px;}");
                    partnerHtmlStr.Append("td {padding-top:6px;padding-bottom:6px;padding-left:6px;}");
                    partnerHtmlStr.Append(".mantine-8drano {font-family: Inter, sans-serif;width: 100%;border-collapse: collapse;caption-side: top;color: rgb(193, 194, 197);line-height: 1.55;}");
                    partnerHtmlStr.Append(".mantine-68nn3q {outline: 0px;display: block;text-decoration: none;color: rgb(193, 194, 197);background-color: rgb(26, 27, 30);box-sizing: border-box;border-radius: 0.25rem;box-shadow: none;}");
                    partnerHtmlStr.Append(".mantine-68nn3q[data-with-border] {border: 0.0625rem solid rgb(55, 58, 64);}");
                    partnerHtmlStr.Append(".mantine-woghk7 {font-family: Inter, sans-serif;color: inherit;font-size: inherit;line-height: 1.55;text-decoration: none;font-weight: 500;}");
                    partnerHtmlStr.Append(".mantine-1fx8ev9 {background-color: rgb(37, 38, 43);width: 140px;}");
                    partnerHtmlStr.Append(".mantine-1as06hs {font-family: Inter, sans-serif;color: inherit;font-size: inherit;line-height: 1.55;text-decoration: none;}");
                    partnerHtmlStr.Append("table {border-color: inherit;border-collapse: collapse;}");
                    partnerHtmlStr.Append("</style>");
                    partnerHtmlStr.Append("</html>");
                    partnerHtmlSource.Html = partnerHtmlStr.ToString();
                    profile.PartnerDetailsHtmlTable = partnerHtmlSource;
                    profile.ShowPartnerDetailsHtmlTable = true;
                }
                profile.pineappleVisible = profile.Pineapple == 1 ? true : false;
                profile.pineappleCrossVisible = profile.Pineapple == 0 ? true : false;
                profile.AvailabilityButtonLabel = profile.Pineapple == 1 ? "&#x1f34d Available" : "Unavailable";
                profile.Image1Url = "photocamera";
                profile.Image2Url = "photocamera";
                profile.Image3Url = "photocamera";
                profile.Image4Url = "photocamera";
                profile.Image5Url = "photocamera";
                if (profile.MyImages != null)
                {
                    foreach (var item in profile.MyImages)
                    {
                        if (profile.MyImages.IndexOf(item)==0)
                        {
                            profile.Image1Url = item.Url;
                            profile.Image1Id = item.Id.ToString();
                        }
                        else if (profile.MyImages.IndexOf(item) == 1)
                        {
                            profile.Image2Url = item.Url;
                            profile.Image2Id = item.Id.ToString();
                        }
                        else if (profile.MyImages.IndexOf(item) == 2)
                        {
                            profile.Image3Url = item.Url;
                            profile.Image3Id = item.Id.ToString();
                        }
                        else if (profile.MyImages.IndexOf(item) == 3)
                        {
                            profile.Image4Url = item.Url;
                            profile.Image4Id = item.Id.ToString();
                        }
                        else if (profile.MyImages.IndexOf(item) == 4)
                        {
                            profile.Image5Url = item.Url;
                            profile.Image5Id = item.Id.ToString();
                        }
                    }
                }
                profile.PrivateImage1Url = "photocamera";
                profile.PrivateImage2Url = "photocamera";
                profile.PrivateImage3Url = "photocamera";
                profile.PrivateImage4Url = "photocamera";
                profile.PrivateImage5Url = "photocamera";
                if (profile.MyPrivateImagesList != null)
                {
                    foreach (var item in profile.MyPrivateImagesList)
                    {
                        if (profile.MyPrivateImagesList.IndexOf(item) == 0)
                        {
                            profile.PrivateImage1Url = item.Url;
                            profile.PrivateImage1Id = item.Id.ToString();
                        }
                        else if (profile.MyPrivateImagesList.IndexOf(item) == 1)
                        {
                            profile.PrivateImage2Url = item.Url;
                            profile.PrivateImage2Id = item.Id.ToString();
                        }
                        else if (profile.MyPrivateImagesList.IndexOf(item) == 2)
                        {
                            profile.PrivateImage3Url = item.Url;
                            profile.PrivateImage3Id = item.Id.ToString();
                        }
                        else if (profile.MyPrivateImagesList.IndexOf(item) == 3)
                        {
                            profile.PrivateImage4Url = item.Url;
                            profile.PrivateImage4Id = item.Id.ToString();
                        }
                        else if (profile.MyPrivateImagesList.IndexOf(item) == 4)
                        {
                            profile.PrivateImage5Url = item.Url;
                            profile.PrivateImage5Id = item.Id.ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }


            return profile;
        }
    }
}
