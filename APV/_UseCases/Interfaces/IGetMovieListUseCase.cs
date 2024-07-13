using APV.CoreBusiness;

namespace APV.UseCases.Interfaces
{
    public interface IGetMovieListUseCase
    {
        Task<List<Movie>> ExecuteAsync(MovieCategory movieCategory = default);
    }
}