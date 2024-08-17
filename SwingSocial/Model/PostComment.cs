using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Sample.Model
{
    public class PostComment
    {
        public string CommentId { get; set; }
        public string ProfileId { get; set; }
        public string UserName { get; set; }
        public string CommentByUserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Avatar { get; set; }
    }
}
