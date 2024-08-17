using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class WhatsHot
    {
        public string Id { get; set; }
        public string ProfileId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int PostTypeId { get; set; }
        public string PhotoLink { get; set; }
        public string ImageCaption { get; set; }
        public string VideoLink { get; set; }
        public string VideoThumbnail { get; set; }
        public string VideoDescription { get; set; }
        public string VideoTitle { get; set; }
        public int LikesCount { get; set; }
        public bool IsImage { get; internal set; }
        public bool IsVideo { get; internal set; }
        public string LikesButtonLabel { get; internal set; }
        public int CommentsCount { get; set; }
        public string CommentsButtonLabel { get; internal set; }
    }
}
