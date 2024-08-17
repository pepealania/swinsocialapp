using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace SwingSocial.Sample.ViewModel
{
    public class CommentsPageViewModel : BasePageViewModel
    {
        private string _likesButtonLabel;
        private ObservableCollection<PostComment> _postcomments = new ObservableCollection<PostComment>();
        private int _totalComments;
        private bool _postDeleteButtonIsVisible;
        private uint _threshold;

        public CommentsPageViewModel(INavigation navigation,WhatsHot ws)
        {
            Navigation = navigation;

            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeComments();
            GetLikes(ws);
            Threshold = (uint)(App.ScreenWidth / 3);

            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
            DraggingCommand = new Command<DraggingCardEventArgs>(OnDraggingCommand);
            ClearItemsCommand = new Command(OnClearItemsCommand);
            AddItemsCommand = new Command(OnAddItemsCommand);
        }
        public ICommand NavigateCommand { get; private set; }
        private INavigation Navigation { get; set; }

        private async void OnNavigateCommand(Type pageType)
        {
            try
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ObservableCollection<PostComment> PostComments
        {
            get => _postcomments;
            set
            {
                _postcomments = value;
                RaisePropertyChanged();
            }
        }
        
        public string LikesButtonLabel
        {
            get => _likesButtonLabel;
            set
            {
                _likesButtonLabel = value;
                RaisePropertyChanged();
            }
        }
        public bool PostDeleteButtonIsVisible
        {
            get => _postDeleteButtonIsVisible;
            set
            {
                _postDeleteButtonIsVisible = value;
                RaisePropertyChanged();
            }
        }
        
        public int TotalComments
        {
            get => _totalComments;
            set
            {
                _totalComments = value;
                RaisePropertyChanged();
            }
        }

        public uint Threshold
        {
            get => _threshold;
            set
            {
                _threshold = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SwipedCommand { get; }

        public ICommand DraggingCommand { get; }

        public ICommand ClearItemsCommand { get; }

        public ICommand AddItemsCommand { get; }

        private void OnSwipedCommand(SwipedCardEventArgs eventArgs)
        {
        }

        private void OnDraggingCommand(DraggingCardEventArgs eventArgs)
        {
            switch (eventArgs.Position)
            {
                case DraggingCardPosition.Start:
                    return;

                case DraggingCardPosition.UnderThreshold:
                    break;

                case DraggingCardPosition.OverThreshold:
                    break;

                case DraggingCardPosition.FinishedUnderThreshold:
                    return;

                case DraggingCardPosition.FinishedOverThreshold:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnClearItemsCommand()
        {
            PostComments.Clear();
        }

        private void OnAddItemsCommand()
        {
        }
        
        private async void InitializeComments()
        {
            WhatsHotsService mock = new WhatsHotsService();
            List<PostComment> _postComments = await mock.LoadPostComments();
            foreach (var item in _postComments)
            {
                PostComments.Add(item);
            }
            if (_postComments.Count == 0)
            {
                TotalComments = 0;
            }
            else
            {
                TotalComments = _postComments.Count;
            }
            PostDeleteButtonIsVisible = SwipeCardView.UsrName == "chrisnleslie" || CommentsPage.PostUserName== SwipeCardView.UsrName ? true : false;
        }

        private async void GetLikes(WhatsHot ws)
        {
            WhatsHotsService service = new WhatsHotsService();
            string totalLikes = await service.GetLikesFromApi(ws);

            LikesButtonLabel = "Like " + totalLikes;

        }



    }
}