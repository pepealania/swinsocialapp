using SwingSocial.Sample.Model;
using SwingSocial.Sample.Services;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
//using static Android.Provider.ContactsContract;

namespace SwingSocial.Sample.ViewModel
{
    internal class SearchPageViewModel : BasePageViewModel
    {
        private ObservableCollection<ProfileEntity> _searchedprofiles = new ObservableCollection<ProfileEntity>();
        private bool _noResultsMessageIsVisible;
        public SearchPageViewModel(INavigation navigation, ProfileEntity p)
        {
            Navigation = navigation;
            NavigateCommand = new Command<Type>(OnNavigateCommand);
            InitializeSearchedProfiles(p);
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

        public ObservableCollection<ProfileEntity> SearchedProfiles
        {
            get => _searchedprofiles;
            set
            {
                _searchedprofiles = value;
                RaisePropertyChanged();
            }
        }

        public bool NoResultsMessageIsVisible
        {
            get => _noResultsMessageIsVisible;
            set
            {
                _noResultsMessageIsVisible = value;
                RaisePropertyChanged();
            }
        }

        private async void InitializeSearchedProfiles(ProfileEntity p)
        {
            UsersMock userMock = new UsersMock();
            List<ProfileEntity> profiles = await userMock.LoadSearchedProfiles(p);
            foreach (var profile in profiles)
            {
                SearchedProfiles.Add(profile);
            }
            if (profiles.Count==0)
            {
                NoResultsMessageIsVisible = true;
            }
        }
        
    }
}
