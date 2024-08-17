using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

using static Android.Provider.CalendarContract;

namespace SwingSocial.Sample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Community: ContentPage
    {
        private const string ExpandAnimationName = "ExpandAnimation";
        private const string CollapseAnimationName = "CollapseAnimation";
        private const double SlideAnimationDuration = 0.25;
        //private const int AnimationDuration = 600;
        private const int AnimationDuration = 600;
        private const double PageScale = 0.98;
        //private const double PageTranslation = 0.35;
        private const double PageTranslation = 0.15;
        private readonly IEnumerable<Xamarin.Forms.View> _menuItemsView;
        private bool _isAnimationRun;
        private double _safeInsetsTop;
        private static string _currentPageRight="Events";
        string currentPineapple = "pineapple.gif";
        //public static VideoPlayer currentVideoPlayer;
        static ExplorePageViewModel ExplorePageViewModel { get; set; }
        public Community()
        {
            try
            {
                
                InitializeComponent();
                ExplorePageViewModel = new ExplorePageViewModel(Navigation);
                BindingContext = ExplorePageViewModel;
                pineapple.Source = currentPineapple;
                EventsListView.ItemTapped += EventsListView_ItemTappedAsync;
                Shell.SetTabBarIsVisible(this, false);
                _currentPageRight = "What's Hot";
                ListTitle.Text = "What's Hot";
                TopGrid.ColumnDefinitions[0].Width = 310;
            }
            catch (Exception ex)
            {

                throw;
            }

            _menuItemsView = new[] { (Xamarin.Forms.View)UsersIcon, StarIcon, MessageIcon, LikeMeIcon, SettingsIcon, SettingsIcon1, SettingsIcon2, SettingsIcon3 };

                OnShowMenu(null, null);

        }

        private async void EventsListView_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {             
            Event currentEvent = e.Item as Event;
            string result = currentEvent.Name;
            var secondPage = new EventView(currentEvent);
            await Navigation.PushAsync(secondPage);
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

            if (Device.RuntimePlatform == Device.iOS)
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                _safeInsetsTop = safeInsets.Top;
                ToolbarSafeAreaRow.Height = MenuSafeAreaRow.Height = _safeInsetsTop;
            }
            //if (currentVideoPlayer != null)
            //{
            //    currentVideoPlayer.DisplayControls = true;

            //}

            if (WhatshotUploadFilePage.TransactionAlive)
            {
                WhatsHotsService whatshotsService = new WhatsHotsService();
                List<WhatsHot> whathots = await whatshotsService.LoadWhatsHots();
                ObservableCollection<WhatsHot> WhatsHots = new ObservableCollection<WhatsHot>();
                foreach (var item in whathots)
                {
                    WhatsHots.Add(item);
                }
                ExplorePageViewModel.WhatsHots = WhatsHots;
                WhatshotUploadFilePage.TransactionAlive = false;
            }

            //foreach (var item in WhatsHotView.ItemTemplate) 
            //{

            //}
            //VideoPlayer currentVideo = WhatsHotView.FindByName<VideoPlayer>("VideoToDisplay");


            //MenuTopRow.Height = MenuBottomRow.Height = Device.Info.ScaledScreenSize.Height * (1 - PageScale) / 2;
            //MenuTopRow.Height = MenuBottomRow.Height = Device.Info.ScaledScreenSize.Height * (1 - PageScale) / 2;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //if (currentVideoPlayer != null)
            //{
            //    currentVideoPlayer.DisplayControls = true;
            //    currentVideoPlayer.Pause();

            //}
            //VideoPlayer currentVideo = WhatsHotView.FindByName<VideoPlayer>("VideoToDisplay");


            //MenuTopRow.Height = MenuBottomRow.Height = Device.Info.ScaledScreenSize.Height * (1 - PageScale) / 2;
            //MenuTopRow.Height = MenuBottomRow.Height = Device.Info.ScaledScreenSize.Height * (1 - PageScale) / 2;
        }

        private async void OnShowMenu(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                ImageButton imageButton = (ImageButton)sender;

                    LearnView.IsVisible = false;
                    TravelView.IsVisible = false;

                if (!EventCreate.EventCreated && !EventCalendarPage.IsCommingFromCalendar)
                {
                    _currentPageRight = "What's Hot";
                    ListTitle.Text = "What's Hot";
                    WhatsHotView.IsVisible = true;
                    WhatsHotViewCreate.IsVisible = true;
                    WhatsHotScrollView.IsVisible = true;
                    EventScrollView.IsVisible = false;
                }
                else
                {
                    ListTitle.Text = "Events";
                    _currentPageRight = "Events";
                    WhatsHotView.IsVisible = false;
                    WhatsHotViewCreate.IsVisible = false;
                    WhatsHotScrollView.IsVisible = false;
                    EventScrollView.IsVisible = true;

                }

            }
            else if (!(sender is Button))
            {
                LearnView.IsVisible = false;
                TravelView.IsVisible = false;

                if (!EventCreate.EventCreated && !EventCalendarPage.IsCommingFromCalendar)
                {
                    _currentPageRight = "What's Hot";
                    ListTitle.Text = "What's Hot";
                    WhatsHotView.IsVisible = true;
                    WhatsHotViewCreate.IsVisible = true;
                    WhatsHotScrollView.IsVisible = true;
                    EventScrollView.IsVisible = false;

                }
                else
                {
                    EventsService eventsService = new EventsService();
                    List<Event> events = new List<Event>();
                    if (EventCalendarPage.IsCommingFromCalendar)
                    {
                        events = await eventsService.EventsByMonth(EventCalendarPage.month,EventCalendarPage.year);
                    }
                    else
                    {
                        events = await eventsService.LoadUserEvents();
                    }
                    
                    ObservableCollection<Event> Events = new ObservableCollection<Event>();
                    foreach (var item in events)
                    {
                        Events.Add(item);
                    }
                    ExplorePageViewModel.Events = Events;
                    ListTitle.Text = "Events";
                    _currentPageRight = "Events";
                    WhatsHotView.IsVisible = false;
                    WhatsHotViewCreate.IsVisible = false;
                    WhatsHotScrollView.IsVisible = false;

                    EventScrollView.IsVisible = true;
                    EventCreate.EventCreated = false;
                }

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

        //private async void OnShowLikeds(object sender, EventArgs e)
        //{
        //    if (sender is ImageButton)
        //    {

        //    }
        //    ListTitle.Text = "Likes";
        //    _currentPageRight = "Likes";
        //    UserListView.IsVisible = false;
        //    ProfilesListView.IsVisible = true;
        //    MaybeProfilesListView.IsVisible = false;
        //    NopeProfilesListView.IsVisible = false;
        //    FriendProfilesListView.IsVisible = false;
        //    LearnView.IsVisible = false;
        //    TravelView.IsVisible = false;
        //    WhatsHotView.IsVisible = false;
        //    WhatsHotViewCreate.IsVisible = false;
        //    WhatsHotScrollView.IsVisible = false;

        //    if (_isAnimationRun)
        //        return;

        //    _isAnimationRun = true;
        //    var animationDuration = AnimationDuration;
        //    if (Page.Scale < 1)
        //    {
        //        animationDuration = (int)(AnimationDuration * SlideAnimationDuration);
        //        GetCollapseAnimation().Commit(this, CollapseAnimationName, 16,
        //            (uint)(AnimationDuration * SlideAnimationDuration),
        //            Easing.Linear,
        //            null, () => false);
        //    }
        //    else
        //    {
        //        GetExpandAnimation().Commit(this, ExpandAnimationName, 16,
        //            AnimationDuration,
        //            Easing.Linear,
        //            null, () => false);
        //    }

        //    await Task.Delay(animationDuration);
        //    _isAnimationRun = false;
        //}

        //private async void OnShowLikedsMe(object sender, EventArgs e)
        //{
        //    if (sender is ImageButton)
        //    {

        //    }
        //    ListTitle.Text = "Likes Me";
        //    _currentPageRight = "Likes Me";
        //    UserListView.IsVisible = false;
        //    ProfilesListView.IsVisible = true;
        //    MaybeProfilesListView.IsVisible = false;
        //    NopeProfilesListView.IsVisible = false;
        //    FriendProfilesListView.IsVisible = false;
        //    LearnView.IsVisible = false;
        //    TravelView.IsVisible = false;
        //    WhatsHotView.IsVisible = false;
        //    WhatsHotViewCreate.IsVisible = false;
        //    WhatsHotScrollView.IsVisible = false;

        //    if (_isAnimationRun)
        //        return;

        //    _isAnimationRun = true;
        //    var animationDuration = AnimationDuration;
        //    if (Page.Scale < 1)
        //    {
        //        animationDuration = (int)(AnimationDuration * SlideAnimationDuration);
        //        GetCollapseAnimation().Commit(this, CollapseAnimationName, 16,
        //            (uint)(AnimationDuration * SlideAnimationDuration),
        //            Easing.Linear,
        //            null, () => false);
        //    }
        //    else
        //    {
        //        GetExpandAnimation().Commit(this, ExpandAnimationName, 16,
        //            AnimationDuration,
        //            Easing.Linear,
        //            null, () => false);
        //    }

        //    await Task.Delay(animationDuration);
        //    _isAnimationRun = false;
        //}

        private async void OnShowEventsView(object sender, EventArgs e)
        {
            //if (currentVideoPlayer != null)
            //{
            //    currentVideoPlayer.Pause();
            //}
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Events";
            _currentPageRight = "Events";
            WhatsHotView.IsVisible = false;
            WhatsHotViewCreate.IsVisible = false;
            WhatsHotScrollView.IsVisible = false;

            LearnView.IsVisible = false;
            EventScrollView.IsVisible = true;
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
        private async void OnShowWhatsHotView(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "What's Hot";
            _currentPageRight = "What's Hot";
            WhatsHotView.IsVisible = true;
            WhatsHotViewCreate.IsVisible = true;
            WhatsHotScrollView.IsVisible = true;

            LearnView.IsVisible = false;
            EventScrollView.IsVisible = false;
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
            //if (currentVideoPlayer != null)
            //{
            //    currentVideoPlayer.Pause();
            //}
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Learn";
            _currentPageRight = "Learn";
            LearnView.IsVisible = true;
            EventScrollView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            WhatsHotViewCreate.IsVisible = false;
            WhatsHotScrollView.IsVisible = false;

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
            //if (currentVideoPlayer != null)
            //{
            //    currentVideoPlayer.Pause();
            //}
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Travel";
            _currentPageRight = "Travel";
            TravelView.IsVisible = true;
            LearnView.IsVisible = false;
            EventScrollView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            WhatsHotViewCreate.IsVisible = false;
            WhatsHotScrollView.IsVisible = false;

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

        //private async void OnShowMaybes(object sender, EventArgs e)
        //{
        //    if (sender is ImageButton)
        //    {

        //    }
        //    ListTitle.Text = "Maybe";
        //    _currentPageRight = "Maybe";
        //    UserListView.IsVisible = false;
        //    MaybeProfilesListView.IsVisible = true;
        //    ProfilesListView.IsVisible = false;
        //    NopeProfilesListView.IsVisible = false;
        //    FriendProfilesListView.IsVisible = false;
        //    LearnView.IsVisible = false;
        //    TravelView.IsVisible = false;
        //    WhatsHotView.IsVisible = false;
        //    WhatsHotViewCreate.IsVisible = false;
        //    WhatsHotScrollView.IsVisible = false;

        //    if (_isAnimationRun)
        //        return;

        //    _isAnimationRun = true;
        //    var animationDuration = AnimationDuration;
        //    if (Page.Scale < 1)
        //    {
        //        animationDuration = (int)(AnimationDuration * SlideAnimationDuration);
        //        GetCollapseAnimation().Commit(this, CollapseAnimationName, 16,
        //            (uint)(AnimationDuration * SlideAnimationDuration),
        //            Easing.Linear,
        //            null, () => false);
        //    }
        //    else
        //    {
        //        GetExpandAnimation().Commit(this, ExpandAnimationName, 16,
        //            AnimationDuration,
        //            Easing.Linear,
        //            null, () => false);
        //    }

        //    await Task.Delay(animationDuration);
        //    _isAnimationRun = false;
        //}

        //private async void OnShowNopeds(object sender, EventArgs e)
        //{
        //    if (sender is ImageButton)
        //    {

        //    }
        //    ListTitle.Text = "Denied";
        //    _currentPageRight = "Denied";
        //    UserListView.IsVisible = false;
        //    NopeProfilesListView.IsVisible = true;
        //    ProfilesListView.IsVisible = false;
        //    MaybeProfilesListView.IsVisible = false;
        //    FriendProfilesListView.IsVisible = false;
        //    LearnView.IsVisible = false;
        //    TravelView.IsVisible = false;
        //    WhatsHotView.IsVisible = false;
        //    WhatsHotViewCreate.IsVisible = false;
        //    WhatsHotScrollView.IsVisible = false;

        //    if (_isAnimationRun)
        //        return;

        //    _isAnimationRun = true;
        //    var animationDuration = AnimationDuration;
        //    if (Page.Scale < 1)
        //    {
        //        animationDuration = (int)(AnimationDuration * SlideAnimationDuration);
        //        GetCollapseAnimation().Commit(this, CollapseAnimationName, 16,
        //            (uint)(AnimationDuration * SlideAnimationDuration),
        //            Easing.Linear,
        //            null, () => false);
        //    }
        //    else
        //    {
        //        GetExpandAnimation().Commit(this, ExpandAnimationName, 16,
        //            AnimationDuration,
        //            Easing.Linear,
        //            null, () => false);
        //    }

        //    await Task.Delay(animationDuration);
        //    _isAnimationRun = false;
        //}

        private async void OnShowFriends(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {

            }
            ListTitle.Text = "Friends";
            _currentPageRight = "Friends";
            EventScrollView.IsVisible = false;
            LearnView.IsVisible = false;
            TravelView.IsVisible = false;
            WhatsHotView.IsVisible = false;
            WhatsHotViewCreate.IsVisible = false;
            WhatsHotScrollView.IsVisible = false;

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

            //if (currentVideoPlayer!=null)
            //{
            //    currentVideoPlayer.Pause();
            //}

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
            //var animation = new Animation
            //{
            //    {0, SlideAnimationDuration, new Animation(v => ToolbarSafeAreaRow.Height = v, _safeInsetsTop, 0)},
            //    {
            //        0, SlideAnimationDuration,
            //        new Animation(v => Page.TranslationX = v, 0, Device.Info.ScaledScreenSize.Width * PageTranslation)
            //    },
            //    {0, SlideAnimationDuration, new Animation(v => Page.Scale = v, 1, PageScale)},
            //    {
            //        0, SlideAnimationDuration,
            //        new Animation(v => Page.Margin = new Thickness(0, v, 0, 0), 0, _safeInsetsTop)
            //    },
            //    {0, SlideAnimationDuration, new Animation(v => Page.CornerRadius = (float) v, 0, 5)}
            //};

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

            //var animation = new Animation
            //{
            //    {0, 1, new Animation(v => ToolbarSafeAreaRow.Height = v, 0, _safeInsetsTop)},
            //    {0, 1, new Animation(v => Page.TranslationX = v, Device.Info.ScaledScreenSize.Width * PageTranslation, 0)},
            //    {0, 1, new Animation(v => Page.Scale = v, PageScale, 1)},
            //    {0, 1, new Animation(v => Page.Margin = new Thickness(0, v, 0, 0), _safeInsetsTop, 0)},
            //    {0, 1, new Animation(v => Page.CornerRadius = (float) v, 5, 0)}
            //};
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
            var secondPage = new MessagesMenuTopPage();
            await Navigation.PushAsync(secondPage);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_LikeTapped(object sender, EventArgs e)
        {
            var b = sender as Button;
            var selectedItem = b.BindingContext as WhatsHot;
            WhatsHotsService service = new WhatsHotsService();
            var resutl = await service.UpdateLikeInWhatsHotItem(selectedItem);
            string totalLikes = await service.GetLikesFromApi(selectedItem);
            //ObservableCollection<WhatsHot> ws = new ObservableCollection<WhatsHot>();
            //foreach (var item in ExplorePageViewModel.WhatsHots)
            //{
            //    if (item.Id==selectedItem.Id)
            //    {
            //        item.LikesButtonLabel = "Like " + totalLikes;
            //    }
            //    ws.Add(item);
            //}
            //ExplorePageViewModel.WhatsHots= ws;
            b.Text = "Like " + totalLikes;

        }

        private async void TapGestureRecognizer_CommentTapped(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            
            var selectedItem = lbl.BindingContext as WhatsHot;
            var secondPage = new CommentsPage(selectedItem,lbl);
            await Navigation.PushAsync(secondPage);
        }

        private async void WhatsHot_CreateTapped(object sender, EventArgs e)
        {
            var lbl = sender as Label;

            var selectedItem = lbl.BindingContext as WhatsHot;
            var secondPage = new WhatshotUploadFilePage(selectedItem);
            await Navigation.PushAsync(secondPage);
        }

        //private async void ImageButton_Clicked_1(object sender, EventArgs e)
        //{
        //    var view = (Xamarin.Forms.View)sender;

        //    VideoPlayer currentVideo = view.FindByName<VideoPlayer>("VideoToDisplay");
        //    currentVideo.DisplayControls = false;
        //    currentVideo.Pause();
        //    var selectedButton = sender as ImageButton;
        //    var selectedItem = selectedButton.BindingContext as WhatsHot;
        //    var secondPage = new CommentsPage(selectedItem);
        //    await Navigation.PushAsync(secondPage);

        //}

        //private void VideoToDisplay_PlayerStateChanged(object sender, Octane.Xamarin.Forms.VideoPlayer.Events.VideoPlayerStateChangedEventArgs e)
        //{
        //    VideoPlayer _currentVideoPlayer = (VideoPlayer)sender;
        //    currentVideoPlayer = _currentVideoPlayer;
        //}

        private async void NewEvent_Tapped(object sender, EventArgs e)
        {
            var newPage = new EventCreate();
            await Navigation.PushAsync(newPage);
        }

        private async void OpenCalendar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var nextPage = new EventCalendarPage();
                await Navigation.PushAsync(nextPage);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async void Comments_Clicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var likeButton = view.FindByName<Button>("WhatsHotLike");
            object button=null;
            WhatsHot wh = new WhatsHot();
            if (sender is Button)
            {
                Button b = sender as Button;
                wh = b.CommandParameter as WhatsHot;
                button = likeButton;
            }
            else if (sender is ImageButton) 
            {
                ImageButton b = sender as ImageButton;
                wh = b.CommandParameter as WhatsHot;
                button = likeButton;
            }
            var nextPage = new CommentsPage(wh,button);
            await Navigation.PushAsync(nextPage);
        }
    }
}
