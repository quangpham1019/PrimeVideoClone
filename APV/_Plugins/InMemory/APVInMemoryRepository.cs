using APV._CoreBusiness;
using APV._Plugins.WebAPI.Tmdb.Models;
using APV.CoreBusiness;
using APV.UseCases.PluginInterfaces;
using AutoMapper;

namespace APV._Plugins.InMemory
{
    public class APVInMemoryRepository : IMovieRepository
    {
        private static List<Movie> _movies = new List<Movie>();
        private static List<MovieCategory> _categories = [.. Enum.GetValues<MovieCategory>()];
        private static Random _R = new Random();

        public APVInMemoryRepository()
        {

            //for(int i = 0; i < 10; i++)
            //{
            //    List<MovieCategory> curCategories = new List<MovieCategory>();
            //    while (curCategories.Count < 2)
            //    {
            //        MovieCategory curCategory = _categories[_R.Next(_categories.Count)];
            //        if (!curCategories.Contains(curCategory))
            //        {
            //            curCategories.Add(curCategory);
            //        }
            //    }

            //    _movies.Add(new Movie
            //    {
            //        Id = i,
            //        Title = $"Movie {i} title",
            //        Background = $"Movie {i} background",
            //        Description = $"Movie {i} description",
            //        Categories = curCategories
            //    });
            //}
        }

        public Task<List<Movie>> GetMoviesByCategory(MovieCategory movieCategory)
        {
            if (movieCategory == default)
            {
                return Task.FromResult(_movies);
            }

            List<Movie> movies = _movies.Where(m => m.Categories.Contains(movieCategory)).ToList();
            return Task.FromResult(movies);
        }

        public Task<MovieDetails> GetMovieById(int movieId)
        {
            //Movie movieFromDb = _movies.FirstOrDefault(m => m.Id == movieId);

            //if (movieFromDb != null)
            //{
            //    return Task.FromResult(new Movie
            //    {
            //        Id = movieFromDb.Id,
            //        Title = movieFromDb.Title,
            //        Description = movieFromDb.Description,
            //        Background = movieFromDb.Background,
            //        Categories = new List<MovieCategory>(movieFromDb.Categories)
            //    });
            //}

            return null;
        }

        public Task<List<Genre>> GetGenres()
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetMoviesByGenre(Genre genre)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetMoviesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }
    }
}
