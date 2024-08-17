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

namespace SwingSocial.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Explore: ContentPage
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
        public Explore()
        {
            try
            {
                InitializeComponent();
                BindingContext = new ExplorePageViewModel(Navigation);
                pineapple.Source = currentPineapple;
                Shell.SetTabBarIsVisible(this, false);
            }
            catch (Exception ex)
            {

                throw;
            }

            //UserListView.ItemsSource = new List<User>
            //{
            //    new User { Name = "Wonderland Party Group Presents Revenge of the Nerds", Description = "Boogie Nights", Photo = "event1.png", Time = Convert.ToDateTime("2024-03-10T02:00:31.14Z") },
            //    new User { Name = "Velvet Seduction Kim Swallows Birthday Bash", Description = "Casino Royale", Photo = "event2.png", Time = Convert.ToDateTime("2024-03-03T02:00:56.387Z") },
            //    new User { Name = "Wonderland Party Group Presents: Carnivale of Carnal Delights", Description = "Horrible Bosses", Photo = "event3.png", Time = Convert.ToDateTime("2024-02-11T02:00:33.438Z") },
            //    new User { Name = "ClubGiggity's Pajama Palooza Weekend Takeover", Description = "National Treasure", Photo = "event4.png", Time = Convert.ToDateTime("2024-05-18T00:00:00Z") },
            //    new User { Name = "LatinRose Party Van Trip to Swingers Club", Description = " X-Men: Days of Future Past ", Photo = "event5.png", Time = Convert.ToDateTime("2024-02-03T21:00:41.572Z") }
            //};
            //UserListView.ItemsSource = new List<User>
            //{
            //    new User { Name = "Mark Wahlberg", Description = "Boogie Nights", Photo = "mark_wahlberg.png", Time = DateTime.Now.AddHours(-2.8) },
            //    new User { Name = "Daniel Craig", Description = "Casino Royale", Photo = "daniel_craig.png", Time = DateTime.Now.AddHours(-2.4) },
            //    new User { Name = "Jennifer Aniston", Description = "Horrible Bosses", Photo = "jennifer_aniston.png", Time = DateTime.Now.AddHours(-3.8) },
            //    new User { Name = "Nicolas Cage", Description = "National Treasure", Photo = "nicolas_cage.png", Time = DateTime.Now.AddHours(-3.8) },
            //    new User { Name = "Halle Berry", Description = " X-Men: Days of Future Past ", Photo = "halle_berry.png", Time = DateTime.Now.AddHours(-4.8) },
            //    new User { Name = "Samuel L. Jackson", Description = "Avengers: Infinity War", Photo = "samuel_l_jackson.png", Time = DateTime.Now.AddHours(-3.8) },
            //    new User { Name = "Glenn Close", Description = "The Girl With All The Gifts", Photo = "glenn_close.png", Time = DateTime.Now.AddHours(-3.8) },
            //    new User { Name = "Matt Damon", Description = "Saving Private Ryan", Photo = "matt_damon.png", Time = DateTime.Now.AddHours(-2) }
            //};

            _menuItemsView = new[] { (Xamarin.Forms.View)UsersIcon, StarIcon, MessageIcon, LikeMeIcon, SettingsIcon, SettingsIcon1, SettingsIcon2, SettingsIcon3, SettingsIcon4, LearnIcon };
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
                _currentPageRight = "Events";
                ListTitle.Text = "Events";
                ProfilesListView.IsVisible = false;
                MaybeProfilesListView.IsVisible = false;
                NopeProfilesListView.IsVisible = false;
                UserListView.IsVisible = true;
                FriendProfilesListView.IsVisible = false;
                LearnView.IsVisible = false;
                TravelView.IsVisible = false;
                WhatsHotView.IsVisible = false;
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

        private async void OnShowLikeds(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Likes";
            _currentPageRight = "Likes";
            UserListView.IsVisible = false;
            ProfilesListView.IsVisible = true;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;

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

        private async void OnShowLikedsMe(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Likes Me";
            _currentPageRight = "Likes Me";
            UserListView.IsVisible = false;
            ProfilesListView.IsVisible = true;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;

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


        private async void OnShowWhatsHotView(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "What's Hot";
            _currentPageRight = "What's Hot";
            WhatsHotView.IsVisible = true;
            LearnView.IsVisible = false;
            UserListView.IsVisible = false;
            ProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            TravelView.IsVisible = false;
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
        private async void OnShowLearnView(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Learn";
            _currentPageRight = "Learn";
            LearnView.IsVisible = true;
            UserListView.IsVisible = false;
            ProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;

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

        private async void OnShowTravelView(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Travel";
            _currentPageRight = "Travel";
            TravelView.IsVisible = true;
            LearnView.IsVisible = false;
            UserListView.IsVisible = false;
            ProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            WhatsHotView.IsVisible = false;

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

        private async void OnShowMaybes(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Maybe";
            _currentPageRight = "Maybe";
            UserListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = true;
            ProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;

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

        private async void OnShowNopeds(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Denied";
            _currentPageRight = "Denied";
            UserListView.IsVisible = false;
            NopeProfilesListView.IsVisible = true;
            ProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;

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

        private async void OnShowFriends(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Friends";
            _currentPageRight = "Friends";
            UserListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            ProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = true;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;

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
                {0, SlideAnimationDuration, new Animation(v => ToolbarSafeAreaRow.Height = v, _safeInsetsTop, 0)},
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
            EventsBack.IsVisible = true;
            Shell.SetNavBarIsVisible(this, false);
            DetailLogo.IsVisible = true;

            var animation = new Animation
            {
                {0, 1, new Animation(v => ToolbarSafeAreaRow.Height = v, 0, _safeInsetsTop)},
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
            //SwipeCardView.InvokeSwipe(SwipeCardDirection.Left);
            var navigationParams = new Dictionary<string, object>
            {
            };
            await Shell.Current.GoToAsync("ChatPage", true);
        }
    }
}
