using SwingSocial.Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Model.Factory
{
    internal abstract class GeneralViewModelFactory
    {
        public abstract IGeneralViewModel FactoryMethod(INavigation navigation);
    }
}
