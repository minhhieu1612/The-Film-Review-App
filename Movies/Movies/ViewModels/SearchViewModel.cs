using Movies.ViewModels.Base;
using System.Threading.Tasks;
using Movies.Models.Movie;
using Movies.Services.Movies;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Movies.Services.Navigation;
using System;
using Movies.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Movies.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {

        private ObservableCollection<Movie> _moviesSearch;

        private IMoviesService _moviesService;
        private INavigationService _navigationService;

        public SearchViewModel(
            IMoviesService moviesService,
            INavigationService navigationService)
        {
            _moviesService = moviesService;
            _navigationService = navigationService;
        }
        public ICommand MovieDetailCommand => new Command<Movie>(MovieDetailAsync);


        public ICommand SearchCommand => new Command<string>(Search);


        public ObservableCollection<Movie> SearchMovie
        {
            get { return _moviesSearch; }
            set
            {
                _moviesSearch = value;
                OnPropertyChanged();
            }
        }


        //public override async Task InitializeAsync(object navigationData)
        //{
        //    IsBusy = true;
        //    var _moviesSearch = await _moviesService.GetPopularAsync();
        //    SearchMovie = new ObservableCollection<Movie>(
        //       _moviesSearch.Results);
        //    IsBusy = false;
        //} 
        private async void Search(string query)
        {
            if (query != null)
            {
                IsBusy = true;
                var _moviesSearch = await _moviesService.GetSearchMovieAsync(query);
                SearchMovie = new ObservableCollection<Movie>(
                    _moviesSearch.Results);
                await _navigationService.NavigateToAsync<SearchResultsViewModel>(query);
                IsBusy = false;
            
            }
        }
       
        private async void MovieDetailAsync(object obj)
        {
            var movie = obj as Movie;

            if (movie != null)
            {
                await _navigationService.NavigateToAsync<DetailViewModel>(movie);
            }
        }
    }
}