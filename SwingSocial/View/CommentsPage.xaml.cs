using MLToolkit.Forms.SwipeCardView;
using MLToolkit.Forms.SwipeCardView.Core;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SwingSocial.Sample.View
{
    public partial class CommentsPage : ContentPage
    {
        string currentPineapple = "pineapple.gif";
        public static string PostId;
        public static string PostUserName;
        private WhatsHot _whatsHot;
        object _button;
        public CommentsPage(WhatsHot whatsHot,object button)
        {
            InitializeComponent();
            _button = button;
            PostId = whatsHot.Id;
            PostUserName = whatsHot.UserName;
            _whatsHot = whatsHot;
            BindingContext = new CommentsPageViewModel(Navigation,whatsHot);
            commentsImage.Source = whatsHot.PhotoLink;
            commentsLabel.Text = whatsHot.UserName;
            commentsLabel.TextColor = Color.White;
            commentsLabel.BackgroundColor = Color.Black;
            pineapple.Source = currentPineapple;
        }

        private async void TapGestureRecognizer_SaveCommentTapped(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string commentText = b.CommandParameter as string;
            //string commentText = (string)((TappedEventArgs)e).Parameter;
            WhatsHotsService whatsHotsService = new WhatsHotsService();
            var result = await whatsHotsService.InsertComment(commentText);
            commentEmtry.Text = String.Empty;
            BindingContext = new CommentsPageViewModel(Navigation,_whatsHot);
            string totalComments = await whatsHotsService.GetCommentsFromApi(_whatsHot); 

            if (_button is Button) 
            {
                ((Button)_button).Text = "Comments " + totalComments;
            }
        }

        private async void DeletePost_Clicked(object sender, EventArgs e)
        {
            WhatsHot wh = new WhatsHot();
            wh.Id = PostId;
            WhatsHotsService whs = new WhatsHotsService();
            var result = await whs.DeletePost(wh);
            var nextPage = new Community();
            await Navigation.PushAsync(nextPage);
        }

        private async void TapGestureRecognizer_LikeTapped(object sender, EventArgs e)
        {
            Button b = sender as Button;
            WhatsHotsService service = new WhatsHotsService();
            var resutl = await service.UpdateLikeInWhatsHotItem(_whatsHot);
            string totalLikes = await service.GetLikesFromApi(_whatsHot);
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
            if (_button == null)
            {
                _whatsHot.LikesButtonLabel = "Like " + totalLikes;
            }
            else if(_button is Button)
            {
                ((Button)_button).Text = "Like " + totalLikes;
            }
        }

        private async void OnChatClicked(object sender, EventArgs e)
        {
            var secondPage = new MessagesMenuTopPage();
            await Navigation.PushAsync(secondPage);
        }

        private void ReplyClicked(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var ReplyComment = view.FindByName<StackLayout>("ReplyComment");
            Button b = sender as Button;

            if (b.Text=="Reply")
            {
                ReplyComment.IsVisible = true;
                b.Text = "Cancel"; 
            }
            else
            {
                ReplyComment.IsVisible = false;
                b.Text = "Reply";
            }

        }
    }
}