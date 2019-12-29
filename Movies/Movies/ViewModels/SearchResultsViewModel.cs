using System.Threading.Tasks;
using Movies.ViewModels.Base;
using Movies.Services.Movies;
using System.Collections.ObjectModel;
using Movies.Models.Movie;
using System.Linq;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Movies.Services.Navigation;

namespace Movies.ViewModels
{
    public class SearchResultsViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _searchResultsMovies;
        private string query;

        private IMoviesService _moviesService;
        private INavigationService _navigationService;

        public SearchResultsViewModel(
            IMoviesService moviesService,
            INavigationService navigationService)
        {
            _moviesService = moviesService;
            _navigationService = navigationService;
        }

        public ObservableCollection<Movie> SearchResultsMovies
        {
            get { return _searchResultsMovies; }
            set
            {
                _searchResultsMovies = value;
                OnPropertyChanged();
            }
        }

        public string ShowKeySearch
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged();
            }
        }

        public ICommand MovieDetailCommand => new Command<Movie>(MovieDetailAsync);

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            var searchResultsMovies = await _moviesService.GetSearchMovieAsync((string)navigationData);
            SearchResultsMovies = new ObservableCollection<Movie>(
                searchResultsMovies.Results);
            query = (string)navigationData;
            ShowKeySearch = (string)navigationData;
            IsBusy = false;
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