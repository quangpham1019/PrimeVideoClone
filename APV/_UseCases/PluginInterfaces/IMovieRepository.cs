using APV.CoreBusiness;

namespace APV.UseCases.PluginInterfaces
{
    public interface IMovieRepository
    {
        Task<Movie> GetMovieById(int movieId);
        Task<List<Movie>> GetMoviesByCategory(MovieCategory movieCategory);
    }
}