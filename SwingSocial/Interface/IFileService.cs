using System;
using System.Collections.Generic;
using System.Text;

namespace SwingSocial.Sample.Interface
{
    public interface IFileService
    {

        void CreateFile(string textMessage);
        string ReadCookie();
    }
}
