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
    public class ComedyViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _comedyMovie;

        private IMoviesService _moviesService;
        private INavigationService _navigationService;

        public ComedyViewModel(
            IMoviesService moviesService,
            INavigationService navigationService)
        {
            _moviesService = moviesService;
            _navigationService = navigationService;
        }

        public ObservableCollection<Movie> Comedy
        {
            get { return _comedyMovie; }
            set
            {
                _comedyMovie = value;
                OnPropertyChanged();
            }
        }

        public ICommand MovieDetailCommand => new Command<Movie>(MovieDetailAsync);

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            var comedy = await _moviesService.GetMovieComedyGenreAsync();
            Comedy = new ObservableCollection<Movie>(
                comedy.Results);

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