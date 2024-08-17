using SwingSocial.Model.Interface;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwingSocial.Model.Factory
{
    internal class TinderPageViewModelFactory : GeneralViewModelFactory
    {
        public override IGeneralViewModel FactoryMethod(INavigation navigation)
        {
            return new TinderPageViewModel(navigation);
        }
    }
}
