using APV._UseCases.Interfaces;
using APV.CoreBusiness;
using APV.UseCases.PluginInterfaces;

namespace APV._UseCases
{
    public class GetMovieDetailsUseCase : IGetMovieDetailsUseCase
    {
        private readonly IMovieRepository movieRepository;

        public GetMovieDetailsUseCase(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<MovieDetails> ExecuteAsync(int movieId)
        {
            return await this.movieRepository.GetMovieById(movieId);
        }
    }
}
