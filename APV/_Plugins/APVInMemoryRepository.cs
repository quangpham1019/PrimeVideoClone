using APV.CoreBusiness;
using APV.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.Plugins
{
    public class APVInMemoryRepository : IMovieRepository
    {
        public static List<Movie> _movies = new List<Movie>();

        public APVInMemoryRepository()
        {
            _movies = new List<Movie>
            {
                new Movie {Id=1, Title="Movie 1 title", Background="Movie 1 background", Description="Movie 1 description", Categories = new List<MovieCategory> { MovieCategory.Popular, MovieCategory.Trending } },
                new Movie {Id=2, Title="Movie 2 title", Background="Movie 2 background", Description="Movie 2 description", Categories = new List<MovieCategory> { MovieCategory.Popular, MovieCategory.ContinueWatching } },
                new Movie {Id=3, Title="Movie 3 title", Background="Movie 3 background", Description="Movie 3 description", Categories = new List<MovieCategory> { MovieCategory.ContinueWatching, MovieCategory.Popular } },
                new Movie {Id=4, Title="Movie 4 title", Background="Movie 4 background", Description="Movie 4 description", Categories = new List<MovieCategory> { MovieCategory.Popular } },
                new Movie {Id=5, Title="Movie 5 title", Background="Movie 5 background", Description="Movie 5 description", Categories = new List<MovieCategory> { MovieCategory.Trending } },
                new Movie {Id=6, Title="Movie 6 title", Background="Movie 6 background", Description="Movie 6 description", Categories = new List<MovieCategory> { MovieCategory.ContinueWatching, MovieCategory.Trending } },
                new Movie {Id=7, Title="Movie 7 title", Background="Movie 7 background", Description="Movie 7 description", Categories = new List<MovieCategory> { MovieCategory.Popular, MovieCategory.Trending, MovieCategory.ContinueWatching } },
            };
        }

        public Task<List<Movie>> GetMoviesByCategory(MovieCategory movieCategory)
        {
            if (movieCategory == MovieCategory.All)
            {
                return Task.FromResult(_movies);
            }

            List<Movie> movies = _movies.Where(m => m.Categories.Contains(movieCategory)).ToList();
            return Task.FromResult(movies);
        }
    }
}
