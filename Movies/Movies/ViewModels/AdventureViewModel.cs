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
    public class AdventureViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _adventureMovie;

        private IMoviesService _moviesService;
        private INavigationService _navigationService;

        public AdventureViewModel(
            IMoviesService moviesService,
            INavigationService navigationService)
        {
            _moviesService = moviesService;
            _navigationService = navigationService;
        }

        public ObservableCollection<Movie> Adventure
        {
            get { return _adventureMovie; }
            set
            {
                _adventureMovie = value;
                OnPropertyChanged();
            }
        }

        public ICommand MovieDetailCommand => new Command<Movie>(MovieDetailAsync);

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            var adventure = await _moviesService.GetMovieAdventureGenreAsync();
            Adventure = new ObservableCollection<Movie>(
                adventure.Results);

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