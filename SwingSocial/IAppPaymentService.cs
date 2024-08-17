using System;
using System.Collections.Generic;
using System.Text;

namespace SwingSocial.Sample
{
    public interface IAppPaymentService
    {
        string GooglePay(string ammount);
    }
}
