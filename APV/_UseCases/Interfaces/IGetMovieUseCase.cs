using APV.CoreBusiness;

namespace APV._UseCases.Interfaces
{
    public interface IGetMovieUseCase
    {
        Task<Movie> ExecuteAsync(int movieId);
    }
}