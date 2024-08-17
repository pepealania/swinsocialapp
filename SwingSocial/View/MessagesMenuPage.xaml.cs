using SwingSocial.Sample.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using SwingSocial.Sample.Model;
using System.Linq;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Internals;
using SwingSocial.Sample.Services;
using System.Collections.ObjectModel;
using Android.Text.Style;

namespace SwingSocial.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagesMenuPage : ContentPage
    {
        private const string ExpandAnimationName = "ExpandAnimation";
        private const string CollapseAnimationName = "CollapseAnimation";
        private const double SlideAnimationDuration = 0.25;
        //private const int AnimationDuration = 600;
        private const int AnimationDuration = 600;
        private const double PageScale = 0.98;
        //private const double PageTranslation = 0.35;
        private const double PageTranslation = 0.20;
        private readonly IEnumerable<Xamarin.Forms.View> _menuItemsView;
        private bool _isAnimationRun;
        private double _safeInsetsTop;
        private static string _currentPageRight="Events";
        string currentPineapple = "pineapple.gif";
        public MessagesMenuPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = new MessagesMenuPageViewModel(Navigation);
                pineapple.Source = currentPineapple;
                Shell.SetTabBarIsVisible(this, false);
                _currentPageRight = "Chats";
                ListTitle.Text = "Chats";
                TopGrid.ColumnDefinitions[0].Width = 310;
            }
            catch (Exception ex)
            {

                throw;
            }


            _menuItemsView = new[] { (Xamarin.Forms.View)ChatsIcon, EmailsIcon };
            OnShowMenu(null,null);
        }

        private async void OnLearnClicked(object sender, EventArgs e)
        {
            //SwipeCardView.InvokeSwipe(SwipeCardDirection.Left);
            var navigationParams = new Dictionary<string, object>
            {
            };
            await Shell.Current.GoToAsync("LearnPage", true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.iOS)
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                _safeInsetsTop = safeInsets.Top;
                ToolbarSafeAreaRow.Height = MenuSafeAreaRow.Height = _safeInsetsTop;
            }

            //MenuTopRow.Height = MenuBottomRow.Height = Device.Info.ScaledScreenSize.Height * (1 - PageScale) / 2;
            //MenuTopRow.Height = MenuBottomRow.Height = Device.Info.ScaledScreenSize.Height * (1 - PageScale) / 2;
        }
        private async void OnShowMenu(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                ImageButton imageButton = (ImageButton)sender;
                _currentPageRight = "Chats";
                ListTitle.Text = "Chats";
                ChatStacklayoutView.IsVisible = true;
                EMailStacklayoutView.IsVisible = false;
            }
            else
            {

            }


            if (_isAnimationRun)
                return;

            _isAnimationRun = true;
            var animationDuration = AnimationDuration;
            if (Page.Scale < 1)
            {
                animationDuration = (int)(AnimationDuration * SlideAnimationDuration);
                GetCollapseAnimation().Commit(this, CollapseAnimationName, 16,
                    (uint)(AnimationDuration * SlideAnimationDuration),
                    Easing.Linear,
                    null, () => false);
            }
            else
            {
                GetExpandAnimation().Commit(this, ExpandAnimationName, 16,
                    AnimationDuration,
                    Easing.Linear,
                    null, () => false);
            }

            await Task.Delay(animationDuration);
            _isAnimationRun = false;
        }

        private async void OnShowChats(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Chats";
            _currentPageRight = "Chats";
            ChatStacklayoutView.IsVisible = true;
            EMailStacklayoutView.IsVisible = false;
            if (_isAnimationRun)
                return;

            _isAnimationRun = true;
            var animationDuration = AnimationDuration;
            if (Page.Scale < 1)
            {
                animationDuration = (int)(AnimationDuration * SlideAnimationDuration);
                GetCollapseAnimation().Commit(this, CollapseAnimationName, 16,
                    (uint)(AnimationDuration * SlideAnimationDuration),
                    Easing.Linear,
                    null, () => false);
            }
            else
            {
                GetExpandAnimation().Commit(this, ExpandAnimationName, 16,
                    AnimationDuration,
                    Easing.Linear,
                    null, () => false);
            }

            await Task.Delay(animationDuration);
            _isAnimationRun = false;
        }

        private async void OnShowEmails(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Inbox";
            _currentPageRight = "Inbox";
            ChatStacklayoutView.IsVisible = false;
            EMailStacklayoutView.IsVisible = true;

            if (_isAnimationRun)
                return;

            _isAnimationRun = true;
            var animationDuration = AnimationDuration;
            if (Page.Scale < 1)
            {
                animationDuration = (int)(AnimationDuration * SlideAnimationDuration);
                GetCollapseAnimation().Commit(this, CollapseAnimationName, 16,
                    (uint)(AnimationDuration * SlideAnimationDuration),
                    Easing.Linear,
                    null, () => false);
            }
            else
            {
                GetExpandAnimation().Commit(this, ExpandAnimationName, 16,
                    AnimationDuration,
                    Easing.Linear,
                    null, () => false);
            }

            await Task.Delay(animationDuration);
            _isAnimationRun = false;
        }
        private Animation GetExpandAnimation()
        {
            TopGrid.ColumnDefinitions[0].Width = 310;

            //if (_currentPageRight == "Events")
            //{
            //    ListTitle.Text = "Events";
            //    ProfilesListView.IsVisible = false;

            //    UserListView.IsVisible = true;

            //}
            //else
            //{
            //    ListTitle.Text = "Likes";
            //    ProfilesListView.IsVisible = true;

            //    UserListView.IsVisible = false;

            //}
            EventsBack.IsVisible = false;
            Shell.SetNavBarIsVisible(this, true);
            DetailLogo.IsVisible = false;
            var iconAnimationTime = (1 - SlideAnimationDuration) / _menuItemsView.Count();
            var animation = new Animation
            {
                {0, SlideAnimationDuration, new Animation(v => ToolbarSafeAreaRow.Height = 0, _safeInsetsTop, 0)},
                {
                    0, SlideAnimationDuration,
                    new Animation(v => Page.TranslationX = v, 0, Device.Info.ScaledScreenSize.Width * PageTranslation)
                },
                {0, SlideAnimationDuration, new Animation(v => Page.Scale = v, 1, PageScale)},
                {
                    0, SlideAnimationDuration,
                    new Animation(v => Page.Margin = new Thickness(0, v, 0, 0), 0, _safeInsetsTop)
                },
                {0, SlideAnimationDuration, new Animation(v => Page.CornerRadius = (float) v, 0, 5)}
            };

            foreach (var view in _menuItemsView)
            {
                var index = _menuItemsView.IndexOf(view);
                animation.Add(SlideAnimationDuration + iconAnimationTime * index - 0.05,
                    SlideAnimationDuration + iconAnimationTime * (index + 1) - 0.05, new Animation(
                        v => view.Opacity = (float)v, 0, 1));
                animation.Add(SlideAnimationDuration + iconAnimationTime * index,
                    SlideAnimationDuration + iconAnimationTime * (index + 1), new Animation(
                        v => view.TranslationY = (float)v, -10, 0));
            }


            return animation;
        }

        private Animation GetCollapseAnimation()
        {
            TopGrid.ColumnDefinitions[0].Width = 365;

            EventsBack.IsVisible = true;
            Shell.SetNavBarIsVisible(this, false);
            DetailLogo.IsVisible = true;
            var animation = new Animation
            {
                {0, 1, new Animation(v => ToolbarSafeAreaRow.Height = 50, 0, _safeInsetsTop)},
                {0, 1, new Animation(v => Page.TranslationX = v, Device.Info.ScaledScreenSize.Width * PageTranslation, 0)},
                {0, 1, new Animation(v => Page.Scale = v, PageScale, 1)},
                {0, 1, new Animation(v => Page.Margin = new Thickness(0, v, 0, 0), _safeInsetsTop, 0)},
                {0, 1, new Animation(v => Page.CornerRadius = (float) v, 5, 0)}
            };

            foreach (var view in _menuItemsView)
            {
                animation.Add(0, 1, new Animation(
                    v => view.Opacity = (float)v, 1, 0));
                animation.Add(0, 1, new Animation(
                    v => view.TranslationY = (float)v, 0, -10));
            }

            return animation;
        }

        private async void OnChatClicked(object sender, EventArgs e)
        {
            var secondPage = new MessagesMenuPage();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }
        private async void mychatList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Chat chat = e.Item as Chat;
            var secondPage = new ChatPage(chat);
            await Navigation.PushAsync(secondPage);

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
        }
        private async void myprofilesInboxEmailList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Email currentEmail = e.Item as Email;
            var secondPage = new EmailReviewPage(currentEmail);
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }

    }
}
