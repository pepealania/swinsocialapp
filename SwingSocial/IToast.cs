using System;
using System.Collections.Generic;
using System.Text;

namespace SwingSocial.Sample
{
    internal interface IToast
    {
        void ShortToast(string message);
        void LongToast(string message);
    }
}
