using APV.CoreBusiness;
using APV.UseCases.PluginInterfaces;
using APV.Plugins.DataStore.WebAPI.Tmdb.Models;
using System.Net.Http.Json;

namespace APV.Plugins.DataStore.WebAPI.Tmdb
{
    public class APVTmdbRepository : IMovieRepository
    {
        private const string ApiKey = "dca2ff96db192826899015a3a5817827";
        public const string TmdbHttpClientName = "TmdbClient";
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient HttpClient => _httpClientFactory.CreateClient(TmdbHttpClientName);
        private Dictionary<MovieCategory, object> UrlByCategory { get; set; }

        public APVTmdbRepository(IHttpClientFactory httpClientFactory)
        {
            // Initialize UrlByCategory dict
            // Top Rated, Popular, Upcoming, etc.
            UrlByCategory = new Dictionary<MovieCategory, object>
            {
                {MovieCategory.Trending, TmdbURLs.GetTrending },
                {MovieCategory.TopRated, TmdbURLs.GetTopRated }
            };
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MovieDetails> GetMovieById(int movieId)
        {
            string getMovieByIdUri = AppendApiKey(TmdbURLs.GetMovieDetails(movieId));

            var tmdbMovieDetails = await HttpClient.GetFromJsonAsync<TmdbMovieDetails>(getMovieByIdUri);

            MovieDetails resultMovie = TmdbMapper.MapTmdbMovieDetailsToMovieDetails(tmdbMovieDetails);
            return resultMovie;
        }

        
        public async Task<List<Movie>> GetMoviesByCategory(MovieCategory movieCategory)
        {
            if (movieCategory == default)
            {
                return null;
            }
            string getMoviesByCategoryUri = AppendApiKey($"{UrlByCategory[movieCategory]}");

            List<Movie> movieList = await GetQueryResponseAndMapToMovie(getMoviesByCategoryUri);
            
            return movieList;
        }
        public async Task<List<APV.CoreBusiness.Genre>> GetGenres()
        {
            string getGenresUri = AppendApiKey(TmdbURLs.GetMovieGenres);

            var movieGenres = await HttpClient.GetFromJsonAsync<GenresWrapper>(getGenresUri);

            return movieGenres.Genres.ToList();
        }

        public async Task<List<Movie>> GetMoviesByGenre(int genreId)
        {
            string getMoviesByGenreUri = AppendApiKey(TmdbURLs.GetMoviesByGenre(genreId));

            List<Movie> movieList = await GetQueryResponseAndMapToMovie(getMoviesByGenreUri);

            return movieList;
        }

        public async Task<List<Movie>> GetSimilarMovies(int movieId)
        {
            string getSimilarMoviesUri = AppendApiKey(TmdbURLs.GetSimilar(movieId));

            List<Movie> movieList = await GetQueryResponseAndMapToMovie(getSimilarMoviesUri);

            return movieList;
        }

        private static string AppendApiKey(string uri)
        {
            return uri + $"&api_key={ApiKey}";
        }

        private class GenresWrapper
        {
            public IEnumerable<APV.CoreBusiness.Genre> Genres { get; set; }
        }

        async Task<List<Movie>> GetQueryResponseAndMapToMovie(string uri)
        {
            MovieListQueryResponse tmdbMovieListQueryResponse = 
                await HttpClient.GetFromJsonAsync<MovieListQueryResponse>(uri);

            Result[] resultArr = tmdbMovieListQueryResponse.results;

            List<Movie> movieList = TmdbMapper.MapResultArrToMovieList(resultArr);

            return movieList;
        }


    }
}
