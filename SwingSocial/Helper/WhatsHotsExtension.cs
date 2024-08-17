using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class WhatsHotsExtension
    {
        public static List<WhatsHot> AddExtraInfo(this List<WhatsHot> whatshots)
        {
            List<WhatsHot> whathotsOut = new List<WhatsHot>();
            foreach (WhatsHot w in whatshots) {
                if (w.PostTypeId==1)
                {
                    w.IsImage = true;
                    w.ImageCaption = w.ImageCaption == null ? "No Caption" : w.ImageCaption;
                }
                else if (w.PostTypeId==2) 
                {
                    w.IsVideo = true;
                }
                w.LikesButtonLabel = "Like " + w.LikesCount.ToString();
                w.CommentsButtonLabel = "Comment " + w.CommentsCount.ToString();
                whathotsOut.Add(w);
            }

            return whathotsOut;
        }
    }
}