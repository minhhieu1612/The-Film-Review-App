using Movies.Models;
using Movies.Models.Movie;
using Movies.Services.Request;
using System.Threading.Tasks;

namespace Movies.Services.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly IRequestService _requestProvider;

        public MoviesService(IRequestService requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<Movie> FindByIdAsync(int movieId, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}movie/{movieId}?api_key={AppSettings.ApiKey}&language={language}";

            Movie response = await _requestProvider.GetAsync<Movie>(uri);

            return response;
        }

        public async Task<Movie> GetLatestAsync(string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}movie/latest?api_key={AppSettings.ApiKey}&language={language}";

            Movie response = await _requestProvider.GetAsync<Movie>(uri);

            return response;
        }

        public async Task<SearchResponse<Movie>> GetPopularAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}movie/popular?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}";

            SearchResponse<Movie> response = await _requestProvider.GetAsync<SearchResponse<Movie>>(uri);

            return response;
        }
        public async Task<SearchResponse<Movie>> GetMovieActionGenreAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}discover/movie?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}&with_genres=28";

            SearchResponse<Movie> response = await _requestProvider.GetAsync<SearchResponse<Movie>>(uri);

            return response;
        }

        public async Task<SearchResponse<Movie>> GetMovieAnimationGenreAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}discover/movie?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}&with_genres=16";

            SearchResponse<Movie> response = await _requestProvider.GetAsync<SearchResponse<Movie>>(uri);

            return response;
        }

        public async Task<SearchResponse<Movie>> GetMovieAdventureGenreAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}discover/movie?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}&with_genres=12";

            SearchResponse<Movie> response = await _requestProvider.GetAsync<SearchResponse<Movie>>(uri);

            return response;
        }

        public async Task<SearchResponse<Movie>> GetMovieComedyGenreAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}discover/movie?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}&with_genres=35";

            SearchResponse<Movie> response = await _requestProvider.GetAsync<SearchResponse<Movie>>(uri);

            return response;
        }

        public async Task<SearchResponse<Movie>> GetMovieHorrorGenreAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}discover/movie?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}&with_genres=27";

            SearchResponse<Movie> response = await _requestProvider.GetAsync<SearchResponse<Movie>>(uri);

            return response;
        }

        public async Task<SearchResponse<Movie>> GetSearchMovieAsync(string query, int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}search/movie?api_key={AppSettings.ApiKey}&query={query}&language={language}&page={pageNumber}";

            SearchResponse<Movie> response = await _requestProvider.GetAsync<SearchResponse<Movie>>(uri);

            return response;
        }

        public async Task<SearchResponse<Movie>> GetTopRatedAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}movie/top_rated?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}";

            SearchResponse<Movie> response = await _requestProvider.GetAsync<SearchResponse<Movie>>(uri);

            return response;
        }

        public async Task<SearchResponse<Movie>> GetUpcomingAsync(int pageNumber = 1, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}movie/upcoming?api_key={AppSettings.ApiKey}&language={language}&page={pageNumber}";

            SearchResponse<Movie> response = await _requestProvider.GetAsync<SearchResponse<Movie>>(uri);

            return response;
        }

        public async Task<MovieCredit> GetCreditsAsync(int movieId, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}movie/{movieId}/credits?api_key={AppSettings.ApiKey}&language={language}";

            MovieCredit response = await _requestProvider.GetAsync<MovieCredit>(uri);

            return response;
        }

        public async Task<MovieVideo> GetVideosAsync(int movieId, string language = "en")
        {
            string uri = $"{AppSettings.ApiUrl}movie/{movieId}/videos?api_key={AppSettings.ApiKey}&language={language}";

            MovieVideo response = await _requestProvider.GetAsync<MovieVideo>(uri);

            return response;
        }
    }
}