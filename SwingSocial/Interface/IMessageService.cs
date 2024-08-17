using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwingSocial.Interface
{
    internal interface IMessageService
    {
        Task ShowAsync(string message);
    }
}
