using APV.CoreBusiness;
using APV._UseCases.Interfaces;
using APV.UseCases.PluginInterfaces;

namespace APV._UseCases
{
    public class GetGenresUseCase : IGetGenresUseCase
    {
        private readonly IMovieRepository movieRepository;

        public GetGenresUseCase(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<List<Genre>> ExecuteAsync()
        {
            return await movieRepository.GetGenres();
        }
    }
}
