using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwingSocial.Sample.Services.Navigation
{
    public interface INavigationService
    {
        Task GotoPage(string route);
    }
}
