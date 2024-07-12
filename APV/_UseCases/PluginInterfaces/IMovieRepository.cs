using APV.CoreBusiness;

namespace APV.UseCases.PluginInterfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMoviesByCategory(MovieCategory movieCategory);
    }
}