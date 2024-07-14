using APV._CoreBusiness;
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
