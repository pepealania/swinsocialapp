using DurianCode.PlacesSearchBar;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.ViewModel;
using System;
using System.IO;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SwingSocial.Sample.View
{
    public partial class EventCreate : ContentPage
    {
        static object imageStream;
        static object image1Stream;
        static object image2Stream;
        public static string EventId;
        string currentPineapple = "pineapple.gif";
        static String ftpurl = "ftp://expatcallers.com/swingsocial/events/"; // e.g. ftp://serverip/foldername/foldername
        static String ftpusername = "jose3@expatcallers.com"; // e.g. username
        static String ftppassword = "Peru2020#Peru"; // e.g. password
        static string imageUrl;
        static string coverImageUrl;
        static string extraImagesUrl;
        static string coverImage2Url;
        private string lattitude;
        private string longitude;
        public static bool EventCreated;
        static String urlEvents = "http://www.expatcallers.com/swingsocial/events/";
        static String ftpurlCoverImage = "ftp://expatcallers.com/swingsocial/events/";
        internal static bool TransactionAlive;
        string GooglePlacesApiKey = "AIzaSyAbs5Umnu4RhdgslS73_TKDSV5wkWZnwi0";


        public EventCreate()
        {
            InitializeComponent();
            BindingContext = new EventPageViewModel(Navigation);


            createEventTitle.TextColor = Color.White;
            createEventTitle.BackgroundColor = Color.Black;
            //eventImages.Children.Add(eventImage);

            //var dtPicker = new DateTimePicker()
            //{
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    StringFormat = "HH:mm dd/MM/yyyy"
            //};
            CategoryPicker.ItemsSource = new[]
            {
                "house-party",
                "meet-greet",
                "hotel-takeover"
            };
            search_bar.ApiKey = GooglePlacesApiKey;
            search_bar.Type = PlaceType.All;
            search_bar.Components = new Components("country:us"); // Restrict results to Australia and New Zealand
            search_bar.PlacesRetrieved += Search_Bar_PlacesRetrieved;
            search_bar.TextChanged += Search_Bar_TextChanged;
            search_bar.MinimumSearchText = 2;
            results_list.ItemSelected += Results_List_ItemSelected;
        }

        void Search_Bar_PlacesRetrieved(object sender, AutoCompleteResult result)
        {
            results_list.ItemsSource = result.AutoCompletePlaces;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

            if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
            {
                VenueListStackLayout.IsVisible = true;
                results_list.IsVisible = true;
            }
                
        }

        void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
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
                VenueListStackLayout.IsVisible = true;
                results_list.IsVisible = true;
                spinner.IsRunning = false;
                spinner.IsVisible = false;
            }
        }

        async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var prediction = (AutoCompletePrediction)e.SelectedItem;
            search_bar.Text = prediction.Description;
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


        private void HideShowPartnerTable(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var partnerTableLabel = view.FindByName<Label>("RSVPLabel");
            var RSVPFrame = view.FindByName<Frame>("RSVPFrame");
            var RSVPCollectionHideShow = view.FindByName<ImageButton>("RSVPCollectionHideShow");

            if (RSVPFrame.IsVisible)
            {
                RSVPCollectionHideShow.Source = "circleplus";
                RSVPCollectionHideShow.HeightRequest = 50;
                RSVPFrame.IsVisible = false;
            }
            else
            {
                RSVPCollectionHideShow.Source = "circleminus";
                RSVPFrame.IsVisible = true;
                RSVPCollectionHideShow.HeightRequest = 50;

            }
        }

        private void HideShowTicketsList(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var ticketsLabel = view.FindByName<Label>("TicketsLabel");
            var ticketsListView = view.FindByName<ListView>("TicketsListView");
            var ticketListViewHideShow = view.FindByName<ImageButton>("TicketListViewHideShow");

            var ticketsCheckout = view.FindByName<Frame>("TicketsCheckout");
            if (ticketsListView.IsVisible)
            {
                ticketListViewHideShow.Source = "circleplus";
                ticketListViewHideShow.HeightRequest = 50;
                ticketsListView.IsVisible = false;
                ticketsCheckout.IsVisible = false;
            }
            else
            {
                ticketListViewHideShow.Source = "circleminus";
                ticketsListView.IsVisible = true;
                ticketListViewHideShow.HeightRequest = 50;
                ticketsCheckout.IsVisible = true;
            }
        }

        private void HideShowAttendees(object sender, EventArgs e)
        {
            var view = (Xamarin.Forms.View)sender;

            var attendeesLabel = view.FindByName<Label>("AttendeesLabel");
            var attendeeFrame = view.FindByName<Frame>("AttendeeFrame");
            var attendeesCollectionHideShow = view.FindByName<ImageButton>("AttendeesCollectionHideShow");

            var ticketsCheckout = view.FindByName<Frame>("TicketsCheckout");
            if (attendeeFrame.IsVisible)
            {
                attendeesCollectionHideShow.Source = "circleplus";
                attendeesCollectionHideShow.HeightRequest = 50;
                attendeeFrame.IsVisible = false;
            }
            else
            {
                attendeesCollectionHideShow.Source = "circleminus";
                attendeeFrame.IsVisible = true;
                attendeesCollectionHideShow.HeightRequest = 50;
            }
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_RSVP(object sender, EventArgs e)
        {
            UsersMock usersMock = new UsersMock();
            usersMock.InsertEvenRsvp();
            BindingContext = new EventPageViewModel(Navigation);
            DependencyService.Get<IToast>().ShortToast("RSVP Successfully saved");
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lets the Entry be empty
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (!double.TryParse(e.NewTextValue, out double value))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var imageButton = sender as ImageButton;

            var selectedItem = imageButton.BindingContext as RSVP;
            var secondPage = new ProfilesPage(selectedItem);
            await Navigation.PushAsync(secondPage);
        }

        private void None_Clicked(object sender, EventArgs e)
        {
            NoneButton.BackgroundColor = Color.FromHex("c21164");
            DailyButton.BackgroundColor = Color.FromHex("292a2e");
            WeeklyButton.BackgroundColor = Color.FromHex("292a2e");
            MonthlyButton.BackgroundColor = Color.FromHex("292a2e");
        }

        private void Daily_Clicked(object sender, EventArgs e)
        {
            NoneButton.BackgroundColor = Color.FromHex("292a2e");
            DailyButton.BackgroundColor = Color.FromHex("c21164");
            WeeklyButton.BackgroundColor = Color.FromHex("292a2e");
            MonthlyButton.BackgroundColor = Color.FromHex("292a2e");

        }

        private void Weekly_Clicked(object sender, EventArgs e)
        {
            NoneButton.BackgroundColor = Color.FromHex("292a2e");
            DailyButton.BackgroundColor = Color.FromHex("292a2e");
            WeeklyButton.BackgroundColor = Color.FromHex("c21164");
            MonthlyButton.BackgroundColor = Color.FromHex("292a2e");

        }

        private void Monthly_Clicked(object sender, EventArgs e)
        {
            NoneButton.BackgroundColor = Color.FromHex("292a2e");
            DailyButton.BackgroundColor = Color.FromHex("292a2e");
            WeeklyButton.BackgroundColor = Color.FromHex("292a2e");
            MonthlyButton.BackgroundColor = Color.FromHex("c21164");

        }

        private void CoverPhoto_Tapped(object sender, EventArgs e)
        {
            PickFile();
        }

        async void PickFile()
        {
            // Opening the File Picker - Filter with Jpeg image
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select your picture",
                FileTypes = FilePickerFileType.Images,
            });

            // Here add the code that is being explained in the next step
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var stream2 = await result.OpenReadAsync();
                imageStream = stream;
                ImageToDisplayCoverImage.Source = ImageSource.FromStream(() => stream2);
                //deletephotoFrame.IsVisible = true;
                //UploadFileToFTP((FileStream)imageStream);
                ShowUploadConfirmationFrame.IsVisible = true;
            }
        }

        private async void UploadFileToFTP(FileStream fs)
        {
            try
            {

                string ftpfullpath = ftpurl + Path.GetFileName(fs.Name);
                imageUrl = "http://www.expatcallers.com/swingsocial/events/" + Path.GetFileName(fs.Name);
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                ftpstream.Dispose();
                //ResultToDisplay.Text = "Successfully Uploaded";

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        private async void SaveEvent_Clicked(object sender, EventArgs e)
        {
            //public.event_insert(qrofileid,
            // qtarttime, qendtime,
            // qname,
            // qdescription,
            // qcategory,
            // qisvenuehidden,
            // qvenue,
            //qcoverimageurl ,
            // qemaildescription,
            // qimages -array)
            IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-US");

            Event ev = new Event();
            ev.StartTime = StartDateTime.DateTime;
            ev.StartTimeString = StartDateTime.DateTime.ToString("MM/dd/yyyy HH:mm", iFormatProvider);
            ev.EndTime = StartDateTime.DateTime;
            ev.EndTimeString = EndDateTime.DateTime.ToString("MM/dd/yyyy HH:mm", iFormatProvider);
            ev.Name = EventNameEntry.Text;
            ev.Description = descriptionMeEditor.Text;
            ev.Category = CategoryPicker.SelectedItem.ToString();
            ev.IsVenueHidden = HideAddressCheckbox.IsChecked?1:0;
            ev.Venue = search_bar.Text;
            ev.CoverImageUrl = imageUrl;
            ev.EmailDescription = emailDescriptionEditor.Text;
            ev.Lattitude = lattitude;
            ev.Longitude = longitude;
            //var nextPage = new EventCreatePhotosPage(ev);
            if (extraImagesUrl == null || extraImagesUrl == string.Empty)
            {
                extraImagesUrl = "";
            }
            ev.ImagesString = "''" + extraImagesUrl + "''";
            EventsService epvm = new EventsService();
            var eventId = await epvm.EventInsert(ev);
            await DisplayAlert("Event Status", "Event Successfully created.", "ok");
            EventCreated = true;
            var nextpage = new Community();
            await Navigation.PushAsync(nextpage);
        }

        private void EmailDescriptionBold_Clicked(object sender, EventArgs e)
        {
            emailDescriptionEditor.FontAttributes = FontAttributes.Bold; 
        }
        private void EmailDescriptionItalic_Clicked(object sender, EventArgs e)
        {
            emailDescriptionEditor.FontAttributes = FontAttributes.Italic;
        }

        private void DescriptionBold_Clicked(object sender, EventArgs e)
        {
            descriptionMeEditor.FontAttributes = FontAttributes.Bold;
        }
        private void DescriptionItalic_Clicked(object sender, EventArgs e)
        {
            descriptionMeEditor.FontAttributes = FontAttributes.Italic;
        }


        private async void UploadPhotos_Tapped(object sender, EventArgs e)
        {
            Event ev = new Event();
            var nextPage = new EventUploadPhotosPage(ev);
            await Navigation.PushAsync(nextPage);
        }
        //private void Image1_Tapped(object sender, EventArgs e)
        //{
        //    PickCoverImage1();
        //}
        //async void PickCoverImage1()
        //{
        //    var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        //    {
        //        Title = "Select image",
        //    });

        //    // Here add the code that is being explained in the next step
        //    if (result != null)
        //    {
        //        var streamImage1 = await result.OpenReadAsync();
        //        var stream2Image1 = await result.OpenReadAsync();
        //        image1Stream = streamImage1;
        //        Image1ToDisplay.Source = ImageSource.FromStream(() => stream2Image1);
        //        UploadImage1ToFTP((FileStream)imageStream);

        //    }
        //}

        //private void Image2_Tapped(object sender, EventArgs e)
        //{
        //    PickCoverImage2();
        //}
        //async void PickCoverImage2()
        //{
        //    var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        //    {
        //        Title = "Select image",
        //    });

        //    // Here add the code that is being explained in the next step
        //    if (result != null)
        //    {
        //        var streamImage2 = await result.OpenReadAsync();
        //        var stream2Image2 = await result.OpenReadAsync();
        //        image2Stream = streamImage2;
        //        Image2ToDisplay.Source = ImageSource.FromStream(() => stream2Image2);
        //        UploadImage2ToFTP((FileStream)imageStream);

        //    }
        //}
        //private async void UploadImage1ToFTP(FileStream fs)
        //{
        //    try
        //    {
        //        string ftpfullpath = ftpurlCoverImage + Path.GetFileName(fs.Name);
        //        coverImage1Url = urlEvents + Path.GetFileName(fs.Name);
        //        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
        //        ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

        //        ftp.KeepAlive = true;
        //        ftp.UseBinary = true;
        //        ftp.Method = WebRequestMethods.Ftp.UploadFile;

        //        byte[] buffer = new byte[fs.Length];
        //        fs.Read(buffer, 0, buffer.Length);

        //        Stream ftpstream = ftp.GetRequestStream();
        //        ftpstream.Write(buffer, 0, buffer.Length);
        //        ftpstream.Close();
        //        ftpstream.Dispose();
        //        //ResultToDisplay.Text = "Successfully Uploaded";

        //    }
        //    catch (WebException ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private async void UploadImage2ToFTP(FileStream fs)
        //{
        //    try
        //    {
        //        string ftpfullpath = ftpurlCoverImage + Path.GetFileName(fs.Name);
        //        coverImage2Url = urlEvents + Path.GetFileName(fs.Name);
        //        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
        //        ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

        //        ftp.KeepAlive = true;
        //        ftp.UseBinary = true;
        //        ftp.Method = WebRequestMethods.Ftp.UploadFile;

        //        byte[] buffer = new byte[fs.Length];
        //        fs.Read(buffer, 0, buffer.Length);

        //        Stream ftpstream = ftp.GetRequestStream();
        //        ftpstream.Write(buffer, 0, buffer.Length);
        //        ftpstream.Close();
        //        ftpstream.Dispose();
        //        //ResultToDisplay.Text = "Successfully Uploaded";

        //    }
        //    catch (WebException ex)
        //    {
        //        throw ex;
        //    }
        //}
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            TransactionAlive = true;
            SwingerSocialPage.FromSwingerSocialPage = false;
        }

        protected async override void OnDisappearing()
        { 
            base.OnDisappearing();
            TransactionAlive = false;
        }

        private void ShowPopUp_Tapped(object sender, EventArgs e)
        {
            ShowPopUp();
        }
        private void ShowPopUp()
        {
            PopUpWaitForUpload.IsVisible = true;
            MainScroll.Opacity = 0.5;
        }
        private async void TapGestureRecognizer_TappedConfirmBox(object sender, EventArgs e)
        {
            FileActivityIndicator.IsRunning = true;
            DoUploads();
            SaveEventButton.IsVisible = true;
            ShowUploadConfirmationFrame.IsVisible = false;
        }

        private void TapGestureRecognizerBack_Tapped(object sender, EventArgs e)
        {
            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }

        private async void DoUploads()
        {
            UploadFileToFTP((FileStream)imageStream);

            PopUpWaitForUpload.IsVisible = false;
            MainScroll.Opacity = 1;
        }

        private void VenueSelected(object sender, SelectedItemChangedEventArgs e)
        {
            AutoCompletePrediction au = e.SelectedItem as AutoCompletePrediction;
            search_bar.Text = au.Description;
            VenueListStackLayout.IsVisible = false;

        }
    }
}