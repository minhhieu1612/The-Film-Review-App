using Movies.Models;
using Movies.Models.Movie;
using System.Threading.Tasks;

namespace Movies.Services.Movies
{
    public interface IMoviesService
    {
        Task<Movie> FindByIdAsync(int movieId, string language = "en");

        Task<Movie> GetLatestAsync(string language = "en");

        Task<SearchResponse<Movie>> GetUpcomingAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetTopRatedAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetPopularAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetMovieActionGenreAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetMovieAnimationGenreAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetMovieAdventureGenreAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetMovieComedyGenreAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetMovieHorrorGenreAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetSearchMovieAsync(string query ,int pageNumber = 1, string language = "en");

        Task<MovieCredit> GetCreditsAsync(int movieId, string language = "en");

        Task<MovieVideo> GetVideosAsync(int movieId, string language = "en");
    }
}