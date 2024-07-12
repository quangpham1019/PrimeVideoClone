using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using APV.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.UseCases
{
    public class GetMovieListUseCase : IGetMovieListUseCase
    {
        private readonly IMovieRepository movieRepository;

        public GetMovieListUseCase(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<List<Movie>> ExecuteAsync(MovieCategory movieCategory = default)
        {
            return await this.movieRepository.GetMoviesByCategory(movieCategory);
        }
    }
}
