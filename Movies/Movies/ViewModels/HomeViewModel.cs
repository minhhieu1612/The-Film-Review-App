using System.Threading.Tasks;
using Movies.ViewModels.Base;
using Movies.Services.Movies;
using Movies.Models.Movie;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Movies.Services.Navigation;
using System;

namespace Movies.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _highlight;
        private ObservableCollection<Movie> _topRatedMovies;
        private ObservableCollection<Movie> _popularMovies;

        private IMoviesService _moviesService;
        private INavigationService _navigationService;

        public HomeViewModel(
            IMoviesService moviesService,
            INavigationService navigationService)
        {
            _moviesService = moviesService;
            _navigationService = navigationService;

            TopRatedMovies = new ObservableCollection<Movie>();
        }

        public ObservableCollection<Movie> Highlight
        {
            get { return _highlight; }
            set
            {
                _highlight = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Movie> TopRatedMovies
        {
            get { return _topRatedMovies; }
            set
            {
                _topRatedMovies = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Movie> PopularMovies
        {
            get { return _popularMovies; }
            set
            {
                _popularMovies = value;
                OnPropertyChanged();
            }
        }


        public ICommand MovieDetailCommand => new Command<Movie>(MovieDetailAsync);
        public ICommand TransSearchPage => new Command(Search);
        private async void Search()
        {
            await _navigationService.NavigateToAsync<SearchViewModel>();
            
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            await LoadTopRatedMoviesAync();
            await LoadPopularMoviesAync();
            await LoadHighLightAync();

            IsBusy = false;
        }

        private async Task LoadTopRatedMoviesAync()
        {
            var result = await _moviesService.GetTopRatedAsync();

            TopRatedMovies = new ObservableCollection<Movie>(result.Results.Take(20));
        }

        private async Task LoadPopularMoviesAync()
        {
            var result = await _moviesService.GetPopularAsync();

            PopularMovies = new ObservableCollection<Movie>(result.Results.Take(20));
            
        }
        private async Task LoadHighLightAync()
        {
            var result = await _moviesService.GetPopularAsync();

            Highlight = new ObservableCollection<Movie>(result.Results.Take(1));

        }

        public ICommand PlayCommand => new Command(Play);

        private string _video;

        public string Video
        {
            get { return _video; }
            set
            {
                _video = value;
                OnPropertyChanged();
            }
        }

        private async void Play()
        {
            var result = await _moviesService.GetPopularAsync();

           var movie =  (Movie)result.Results.Take(1);
            var videos = await _moviesService.GetVideosAsync(movie.Id);

            if (videos.Videos.Any())
            {
                var video = videos.Videos.First();
                Video = string.Format("{0}{1}", AppSettings.YouTubeUrl, video.Key);

                if (string.IsNullOrEmpty(Video))
                    return;

                Device.OpenUri(new Uri(Video));
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