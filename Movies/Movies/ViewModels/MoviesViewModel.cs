using System.Threading.Tasks;
using Movies.ViewModels.Base;
using Movies.Services.Movies;
using System.Collections.ObjectModel;
using Movies.Models.Movie;
using Xamarin.Forms;
using System.Windows.Input;
using Movies.Services.Navigation;

namespace Movies.ViewModels
{
    public class MoviesViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _movies;

        private IMoviesService _moviesService;
        private INavigationService _navigationService;

        public MoviesViewModel(
            IMoviesService moviesService,
            INavigationService navigationService)
        {
            _moviesService = moviesService;
            _navigationService = navigationService;
        }
        public ICommand MovieDetailCommand => new Command<Movie>(MovieDetailAsync);
        public ObservableCollection<Movie> Movies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

           var movies = await _moviesService.GetPopularAsync();
            Movies = new ObservableCollection<Movie>(movies.Results);
            MessagingCenter.Send(this, AppSettings.MoviesMessage, Movies as object);

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