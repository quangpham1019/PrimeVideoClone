using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using APV.UseCases.PluginInterfaces;

namespace APV.UseCases
{
    public class GetMovieListUseCase : IGetMovieListUseCase
    {
        private readonly IMovieRepository movieRepository;

        public GetMovieListUseCase(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        // public async Task<List<Movie>> ExecuteAsync(string filterCategory, filter
        public async Task<List<Movie>> ExecuteAsync(MovieCategory movieCategory = default)
        {
            return await movieRepository.GetMoviesByCategory(movieCategory);
        }

        public async Task<List<Movie>> ExecuteAsync(int genreId)
        {
            return await movieRepository.GetMoviesByGenre(genreId);
        }

        public async Task<List<Movie>> ExecuteAsync(int movieId, string filter)
        {
            return await movieRepository.GetSimilarMovies(movieId);
        }
    }
}
