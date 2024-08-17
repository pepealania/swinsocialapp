using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwingSocial.Sample.Helper
{
    public static class EmojisExtension
    {
        public static List<Emoji> AddExtraInfo(this List<Emoji> emojis)
        {
            List<Emoji> emojisOut = new List<Emoji>();
            foreach (Emoji r in emojis) {
                r.HexAmp = r.Hex.Replace("&amp;","&");
                emojisOut.Add(r);
            }

            return emojisOut;
        }
    }
}
