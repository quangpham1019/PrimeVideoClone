using APV._Plugins.WebAPI.Tmdb.Models;
using APV._UseCases.Interfaces;
using APV.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
