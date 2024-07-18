using APV._Plugins.WebAPI.Tmdb.Models;
using APV._UseCases.Interfaces;
using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace APV.ViewModels
{
    public partial class HomePageViewModel : ViewModel
    {
        public readonly IGetMovieListUseCase getMovieListUseCase;
        public readonly IGetGenresUseCase getGenresUseCase;

        List<MovieCategory> MovieCategories { get; set; }

        [ObservableProperty]
        ObservableCollection<MovieRowViewModel> movieRowList;

        [ObservableProperty]
        ObservableCollection<Movie> movieCarousel;

        public HomePageViewModel()
        {
        }

        public HomePageViewModel(
            IGetMovieListUseCase getMovieListUseCase,
            IGetGenresUseCase getGenresUseCase)
        {
            this.getMovieListUseCase = getMovieListUseCase;
            this.getGenresUseCase = getGenresUseCase;

            MovieRowList = [];
            MovieCarousel = [];
            MovieCategories = Enum.GetValues(typeof(MovieCategory)).Cast<MovieCategory>().ToList();

            Task.Run(InitializeMovieRowList);
        }

        public async Task InitializeMovieRowList()
        {
            MovieRowList.Clear();

            List<Genre> genres = await getGenresUseCase.ExecuteAsync();

            List<Movie>[] movieListByCategory = await Task.WhenAll(InitializeGetMovieListByCategoryTasks());
            List<Movie>[] movieListsByGenre = await Task.WhenAll(InitializeGetMovieListByGenreTasks(genres));

            for (int i = 0; i < movieListByCategory.Length; i++)
            {
                if (movieListByCategory[i] is null)
                {
                    continue;
                }
                if (MovieCategories[i] == MovieCategory.Trending)
                {
                    foreach(Movie movie in movieListByCategory[i])
                    {
                        MovieCarousel.Add(movie);
                    }
                    continue;
                }
                MovieRowList.Add(new MovieRowViewModel(MovieCategories[i], movieListByCategory[i]));
            }

            List<Movie>[] movieListsByGenreToRender = new List<Movie>[5];
            List<Genre> genresToRender = new List<Genre>();

            for (int i =0; i < movieListsByGenreToRender.Length; i++ )
            {
                movieListsByGenreToRender[i] = movieListsByGenre[i];
                genresToRender.Add(genres[i]);    
            }
            AddMovieListByGenreToMovieRowList(movieListsByGenreToRender, genresToRender);

        }

        public Task<List<Movie>>[] InitializeGetMovieListByGenreTasks(List<Genre> genres)
        {
            Task<List<Movie>>[] movieListByGenreTasks = new Task<List<Movie>>[genres.Count];
            for (int i = 0; i < movieListByGenreTasks.Length; i++)
            {
                movieListByGenreTasks[i] = getMovieListUseCase.ExecuteAsync(genres[i].Id);
            }

            return movieListByGenreTasks;
        }

        public Task<List<Movie>>[] InitializeGetMovieListByCategoryTasks()
        {
            Task<List<Movie>>[] movieListByCategoryTasks = new Task<List<Movie>>[MovieCategories.Count];
            for (int i = 0; i < movieListByCategoryTasks.Length; i++)
            {
                movieListByCategoryTasks[i] = getMovieListUseCase.ExecuteAsync(MovieCategories[i]);
            }

            return movieListByCategoryTasks;
        }

        public void AddMovieListByGenreToMovieRowList(List<Movie>[] movieListByGenre, List<Genre> genres)
        {
            for (int i = 0; i < movieListByGenre.Length; i++)
            {
                MovieRowList.Add(new MovieRowViewModel(genres[i].Name, movieListByGenre[i]));
            }
        }
    }
}
