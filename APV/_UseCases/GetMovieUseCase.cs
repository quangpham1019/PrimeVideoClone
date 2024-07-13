using APV._UseCases.Interfaces;
using APV.CoreBusiness;
using APV.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV._UseCases
{
    public class GetMovieUseCase : IGetMovieUseCase
    {
        private readonly IMovieRepository movieRepository;

        public GetMovieUseCase(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<Movie> ExecuteAsync(int movieId)
        {
            return await this.movieRepository.GetMovieById(movieId);
        }
    }
}
