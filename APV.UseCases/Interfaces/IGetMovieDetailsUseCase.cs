using APV.CoreBusiness;

namespace APV.UseCases.Interfaces
{
    public interface IGetMovieDetailsUseCase
    {
        Task<MovieDetails> ExecuteAsync(int movieId);
    }
}