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
using DurianCode.PlacesSearchBar;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace SwingSocial.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Matches: ContentPage
    {
        private const string ExpandAnimationName = "ExpandAnimation";
        private const string CollapseAnimationName = "CollapseAnimation";
        private const double SlideAnimationDuration = 0.25;
        //private const int AnimationDuration = 600;
        private const int AnimationDuration = 600;
        private const double PageScale = 0.98;
        //private const double PageTranslation = 0.35;
        private const double PageTranslation = 0.10;
        private readonly IEnumerable<Xamarin.Forms.View> _menuItemsView;
        private bool _isAnimationRun;
        private double _safeInsetsTop;
        private static string _currentPageRight="Events";
        string currentPineapple = "pineapple.gif";
        string GooglePlacesApiKey = "AIzaSyAbs5Umnu4RhdgslS73_TKDSV5wkWZnwi0";
        private string lattitude;
        private string longitude;
        bool firstLoading;
        static MatchesPageViewModel MatchesPageViewModel { get; set; }

        public Matches()
        {
            try
            {
                InitializeComponent();
                MatchesPageViewModel = new MatchesPageViewModel(Navigation);

                BindingContext = MatchesPageViewModel;
                Location.ApiKey = GooglePlacesApiKey;
                Location.Type = PlaceType.All;
                Location.Components = new Components("country:us"); // Restrict results to Australia and New Zealand
                Location.PlacesRetrieved += Location_PlacesRetrieved;
                Location.TextChanged += Location_TextChanged;
                Location.MinimumSearchText = 2;
                results_list.ItemSelected += Results_List_ItemSelected;
                pineapple.Source = currentPineapple;
                Shell.SetTabBarIsVisible(this, false);
                _currentPageRight = "Liked";
                ListTitle.Text = "Liked";
                TopGrid.ColumnDefinitions[0].Width = 310;
                //1951    Couple(M / F)
                //1793    Man
                //1294    Couple
                //352          Woman
                //12      Throuple

                AccountTypesPicker.ItemsSource = new[]
                {
                    "Couple",
                    "Man",
                    "Woman",
                    "Throuple"
                };
                foreach (var item in AccountTypesPicker.Items)
                {
                    item.FontSize = 12;
                }
                AccountTypesPicker.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                throw;
            }

            _menuItemsView = new[] { (Xamarin.Forms.View)UsersIcon, StarIcon, MessageIcon, LikeMeIcon, SettingsIcon, SettingsIcon1, SettingsIcon2, BlockedButton };
            OnShowMenu(null, null);
            OnShowLikeds(null, null);
        }

        void Location_PlacesRetrieved(object sender, AutoCompleteResult result)
        {
            if (firstLoading)
            {
                firstLoading = false;
                return;
            }
            results_list.ItemsSource = result.AutoCompletePlaces;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

            if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
            {
                VenueListStackLayout.IsVisible = true;
                results_list.IsVisible = true;
            }

        }

        void Location_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                VenueListStackLayout.IsVisible = false;
                results_list.IsVisible = false;
                spinner.IsVisible = true;
                spinner.IsRunning = true;
            }
            else
            {
                //VenueListStackLayout.IsVisible = true;
                //results_list.IsVisible = true;
                //spinner.IsRunning = false;
                //spinner.IsVisible = false;
            }
        }

        async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var prediction = (AutoCompletePrediction)e.SelectedItem;
            Location.Text = prediction.Description;
            results_list.SelectedItem = null;

            var place = await Places.GetPlace(prediction.Place_ID, GooglePlacesApiKey);

            if (place != null)
            {
                lattitude = place.Latitude.ToString();
                longitude = place.Longitude.ToString();
                await DisplayAlert(
                    place.Name, string.Format("Lat: {0}\nLon: {1}", place.Latitude, place.Longitude), "OK");
                VenueListStackLayout.IsVisible = false;
                results_list.IsVisible = false;

            }

        }
        private async void OnLearnClicked(object sender, EventArgs e)
        {
            //SwipeCardView.InvokeSwipe(SwipeCardDirection.Left);
            var navigationParams = new Dictionary<string, object>
            {
            };
            await Shell.Current.GoToAsync("LearnPage", true);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            firstLoading = true;

            if (Device.RuntimePlatform == Device.iOS)
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                _safeInsetsTop = safeInsets.Top;
                ToolbarSafeAreaRow.Height = MenuSafeAreaRow.Height = _safeInsetsTop;
            }
            //MenuTopRow.Height = MenuBottomRow.Height = Device.Info.ScaledScreenSize.Height * (1 - PageScale) / 2;
            //MenuTopRow.Height = MenuBottomRow.Height = Device.Info.ScaledScreenSize.Height * (1 - PageScale) / 2;
        }

        private async void OnShowOnline(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                ImageButton imageButton = (ImageButton)sender;
                _currentPageRight = "Online";
                ListTitle.Text = "Online";
                LikedProfilesListView.IsVisible = false;
                MaybeProfilesListView.IsVisible = false;
                NopeProfilesListView.IsVisible = false;
                FriendProfilesListView.IsVisible = false;
                LearnView.IsVisible = false;
                TravelView.IsVisible = false;
                WhatsHotView.IsVisible = false;
                SearchView.IsVisible = false;
                BlockedProfilesListView.IsVisible = false;
                OnlineView.IsVisible = true;
            }
            else
            {

            }
            UsersMock userMock = new UsersMock();
            int typeSelected = SearchResultsPage.TypeSelected;
            List<SwingSocial.Sample.Model.ProfileEntity> profiles = await userMock.GetOnlineList();
            MatchesPageViewModel.OnlineProfiles = new ObservableCollection<SwingSocial.Sample.Model.ProfileEntity>();    
            foreach (var profile in profiles)
            {
                MatchesPageViewModel.OnlineProfiles.Add(profile);
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


        private async void OnShowMenu(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                ImageButton imageButton = (ImageButton)sender;
                _currentPageRight = "Events";
                ListTitle.Text = "Events";
                LikedProfilesListView.IsVisible = false;
                MaybeProfilesListView.IsVisible = false;
                NopeProfilesListView.IsVisible = false;
                FriendProfilesListView.IsVisible = false;
                LearnView.IsVisible = false;
                TravelView.IsVisible = false;
                WhatsHotView.IsVisible = false;
                SearchView.IsVisible = false;
                OnlineView.IsVisible = false;
                BlockedProfilesListView.IsVisible = false;

            }
            else
            {

            }

            UsersMock userMock = new UsersMock();
            List<ProfileEntity> profiles = await userMock.LoadLikedUserProfiles();
            ObservableCollection<ProfileEntity> LikedProfiles = new ObservableCollection<ProfileEntity>();
            foreach (var profile in profiles)
            {
                LikedProfiles.Add(profile);
            }
            MatchesPageViewModel.LikedProfiles = LikedProfiles;
            LikedProfilesListView.IsVisible = true;
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

        private async void OnShowSearch(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "";
            _currentPageRight = "";
            LikedProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            SearchView.IsVisible = true;
            OnlineView.IsVisible = false;
            BlockedProfilesListView.IsVisible = false;

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
            ListTitle.Text = "Liked";
            _currentPageRight = "Liked";
            LikedProfilesListView.IsVisible = true;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            SearchView.IsVisible = false;
            OnlineView.IsVisible = false;
            BlockedProfilesListView.IsVisible = false;
            UsersMock userMock = new UsersMock();
            List<ProfileEntity> profiles = await userMock.LoadLikedUserProfiles();
            MatchesPageViewModel.LikedProfiles = new ObservableCollection<ProfileEntity>();
            foreach (var profile in profiles)
            {
                MatchesPageViewModel.LikedProfiles.Add(profile);
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
            //OnShowMenu(sender, e);
        }

        private async void OnShowBlocked(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Blocked";
            _currentPageRight = "Blocked";
            BlockedProfilesListView.IsVisible = true;
            LikedProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            SearchView.IsVisible = false;
            OnlineView.IsVisible = false;

            UsersMock userMock = new UsersMock();
            List<SwingSocial.Sample.Model.ProfileEntity> profiles = await userMock.LoadBlockedUserProfiles();
            MatchesPageViewModel.BlockedProfiles = new ObservableCollection<SwingSocial.Sample.Model.ProfileEntity>();
            foreach (var profile in profiles)
            {
                MatchesPageViewModel.BlockedProfiles.Add(profile);
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

        private async void OnShowLikedsMe(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Likes Me";
            _currentPageRight = "Likes Me";
            LikedProfilesListView.IsVisible = true;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            SearchView.IsVisible = false;
            OnlineView.IsVisible = false;
            BlockedProfilesListView.IsVisible = false;

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
            LikedProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            SearchView.IsVisible = false;
            OnlineView.IsVisible = false;
            BlockedProfilesListView.IsVisible = false;

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
            LikedProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            SearchView.IsVisible = false;
            OnlineView.IsVisible = false;
            BlockedProfilesListView.IsVisible = false;

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
            MaybeProfilesListView.IsVisible = true;
            LikedProfilesListView.IsVisible = false;
            NopeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            SearchView.IsVisible = false;
            OnlineView.IsVisible = false;
            BlockedProfilesListView.IsVisible = false;

            UsersMock userMock = new UsersMock();
            List<SwingSocial.Sample.Model.ProfileEntity> profiles = await userMock.LoadMaybeUserProfiles();
            MatchesPageViewModel.MaybeProfiles = new ObservableCollection<SwingSocial.Sample.Model.ProfileEntity>();
            foreach (var profile in profiles)
            {
                MatchesPageViewModel.MaybeProfiles.Add(profile);
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

        private async void OnShowNopeds(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Denied";
            _currentPageRight = "Denied";
            NopeProfilesListView.IsVisible = true;
            LikedProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            SearchView.IsVisible = false;
            OnlineView.IsVisible = false;
            BlockedProfilesListView.IsVisible = false;

            UsersMock userMock = new UsersMock();
            List<SwingSocial.Sample.Model.ProfileEntity> profiles = await userMock.LoadNopedUserProfiles();
            MatchesPageViewModel.NopedProfiles = new ObservableCollection<SwingSocial.Sample.Model.ProfileEntity>();
            foreach (var profile in profiles)
            {
                MatchesPageViewModel.NopedProfiles.Add(profile);
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

        private async void OnShowFriends(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Friends";
            _currentPageRight = "Friends";
            NopeProfilesListView.IsVisible = false;
            LikedProfilesListView.IsVisible = false;
            MaybeProfilesListView.IsVisible = false;
            FriendProfilesListView.IsVisible = true;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            SearchView.IsVisible = false;
            OnlineView.IsVisible = false;
            BlockedProfilesListView.IsVisible = false;

            UsersMock userMock = new UsersMock();
            List<SwingSocial.Sample.Model.ProfileEntity> profiles = await userMock.LoadFriendsUserProfiles();
            MatchesPageViewModel.FriendProfiles = new ObservableCollection<SwingSocial.Sample.Model.ProfileEntity>();
            foreach (var profile in profiles)
            {
                MatchesPageViewModel.FriendProfiles.Add(profile);
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
            //var navigationParams = new Dictionary<string, object>
            //{
            //};
            //await Shell.Current.GoToAsync("ChatPage", true);
            var secondPage = new MessagesMenuTopPage();
            //var secondPage = new ChatPage(profile);
            await Navigation.PushAsync(secondPage);
        }
        private async void SearchButton_Clicked(object sender, EventArgs e)
        {

            Model.ProfileEntity p = new Model.ProfileEntity();
            p.Username = SearchUsername.Text ==null?"''":SearchUsername.Text;
            p.AccountTypeInteger = AccountTypesPicker.SelectedIndex + 1;
            p.Location = Location.Text==null?"''":Location.Text;
            p.HisAgeTo = Convert.ToInt32(HisAgeTo.Text);
            p.HisAgeFrom = Convert.ToInt32(HisAgeTo.Text);
            p.HerAgeTo = Convert.ToInt32(HerAgeTo.Text);
            p.HerAgeFrom = Convert.ToInt32(HerAgeFrom.Text);
            p.OnlyWithPhotos = OnlyWithPhotos.IsChecked?1:0;
            p.HisOrientation = HisOrientation.SelectedItem==null?"''":HisOrientation.SelectedItem.ToString();
            p.HerOrientation = HerOrientation.SelectedItem == null ? "''" : HerOrientation.SelectedItem.ToString();
            var nextPage = new SearchResultsPage(p);
            await Navigation.PushAsync(nextPage);
        }

        private async void OnlinesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            SwingSocial.Sample.Model.ProfileEntity p = e.Item as SwingSocial.Sample.Model.ProfileEntity;
            var secondPage = new ProfilesPageOnline(p);
            await Navigation.PushAsync(secondPage);
        }

        private async void LikedProfilesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Model.ProfileEntity p = e.Item as Model.ProfileEntity;
            string idprofile = p.IdPlainString;
            var secondPage = new ProfilesPageLiked(p);
            await Navigation.PushAsync(secondPage);
        }
        
        private async void FriendProfilesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Model.ProfileEntity p = e.Item as Model.ProfileEntity;
            string idprofile = p.IdPlainString;
            var secondPage = new ProfilesPageFriend(p);
            await Navigation.PushAsync(secondPage);
        }
        private async void DeniedProfilesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Model.ProfileEntity p = e.Item as Model.ProfileEntity;
            string idprofile = p.IdPlainString;
            var secondPage = new ProfilesPageDenied(p);
            await Navigation.PushAsync(secondPage);
        }        
        private async void MaybeProfilesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Model.ProfileEntity p = e.Item as Model.ProfileEntity;
            string idprofile = p.IdPlainString;
            var secondPage = new ProfilesPageMaybe(p);
            await Navigation.PushAsync(secondPage);
        }
        private void LocalLikedSearch_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string conversationText = button.CommandParameter.ToString();
            LikedProfilesList.ItemsSource = MatchesPageViewModel.LikedProfiles.Where(x => x.UserName.Contains(conversationText));

        }
        private void LocalDeniedSearch_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string conversationText = button.CommandParameter.ToString();
            DeniedProfilesList.ItemsSource = MatchesPageViewModel.NopedProfiles.Where(x => x.UserName.Contains(conversationText));

        }
        private void LocalMaybeSearch_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string conversationText = button.CommandParameter.ToString();
            MaybeProfilesList.ItemsSource = MatchesPageViewModel.MaybeProfiles.Where(x => x.UserName.Contains(conversationText));

        }
        
        private void LocalOnlineSearch_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string conversationText = button.CommandParameter.ToString();
            OnlinesListView.ItemsSource = MatchesPageViewModel.OnlineProfiles.Where(x => x.UserName.Contains(conversationText));

        }

    }
}
