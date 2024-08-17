using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class RSVPsExtension
    {
        public static List<PostComment> AddExtraInfo(this List<PostComment> postComments)
        {
            List<PostComment> postCommentssOut = new List<PostComment>();
            foreach (PostComment r in postComments) {
                if (r.Avatar==null || r.Avatar=="" || r.Avatar.Contains("data:image/png"))
                {
                    r.Avatar = "noavatar";
                }
                postCommentssOut.Add(r);
            }

            return postCommentssOut;
        }
    }
}
