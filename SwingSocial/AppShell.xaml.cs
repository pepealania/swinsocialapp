using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DemoToolbarIcon
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            //Routing.RegisterRoute("UploadFilePage", typeof(WhatshotUploadFilePage));
            //Routing.RegisterRoute("SwingerSocialPage", typeof(SwingerSocialPage));
            //Routing.RegisterRoute("Explore", typeof(Explore));
            //Routing.RegisterRoute("Community", typeof(Community));
            //Routing.RegisterRoute("Likes", typeof(Matches));
            //Routing.RegisterRoute("ChatPage", typeof(ChatPage));
            //Routing.RegisterRoute("MyChats", typeof(MyChats));
            //Routing.RegisterRoute("LearnPage", typeof(LearnPage));
            //Routing.RegisterRoute("AppShell", typeof(AppShell));
            //Routing.RegisterRoute("MainPage", typeof(MainPage));
            //Routing.RegisterRoute("EventView", typeof(EventView));

            Shell.SetTabBarIsVisible(this, false);


        }
    }
}
