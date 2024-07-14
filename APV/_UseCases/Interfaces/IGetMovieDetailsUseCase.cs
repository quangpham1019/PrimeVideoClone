using APV._CoreBusiness;
using APV.CoreBusiness;

namespace APV._UseCases.Interfaces
{
    public interface IGetMovieDetailsUseCase
    {
        Task<MovieDetails> ExecuteAsync(int movieId);
    }
}