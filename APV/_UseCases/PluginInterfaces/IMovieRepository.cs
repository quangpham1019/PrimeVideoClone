using APV.CoreBusiness;

namespace APV.UseCases.PluginInterfaces
{
    public interface IMovieRepository
    {
        Task<MovieDetails> GetMovieById(int movieId);
        Task<List<Movie>> GetMoviesByCategory(MovieCategory movieCategory);
        Task<List<Genre>> GetGenres();
        Task<List<Movie>> GetMoviesByGenre(int genreId);
        Task<List<Movie>> GetSimilarMovies(int movieId);
    }
}