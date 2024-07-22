namespace APV.Plugins.DataStore.WebAPI.Tmdb
{
    public class TmdbURLs
    {
        public static string GetTrending => "3/trending/all/week?language=en-US";
        public static string GetTopRated => "3/movie/top_rated?language=en-US";
        public static string GetMovieGenres => "3/genre/movie/list?language=en-US";

        public const string NetflixOriginals = "3/discover/tv?language=en-US&with_networks=213";
        public static string GetNetworks => "3/network/list";

        public static string GetMoviesByGenre(int genreId) => $"3/discover/movie?language=en-US&with_genres={genreId}";

        public static string GetNowPlaying(string minDate, string maxDate) => $"3/discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc&with_release_type=2|3&release_date.gte={minDate}&release_date.lte={maxDate}";
        public static string GetTrailers(int movieId, string type = "movie") => $"3/{type ?? "movie"}/{movieId}/videos?language=en-US";
        public static string GetMovieDetails(int movieId, string type = "movie") => $"3/{type ?? "movie"}/{movieId}?language=en-US";
        public static string GetSimilar(int movieId, string type = "movie") => $"3/{type ?? "movie"}/{movieId}/similar?language=en-US";
    }
}
