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
        private readonly IGetMovieListUseCase getMovieListUseCase;
        private readonly IGetGenresUseCase getGenresUseCase;

        List<MovieCategory> MovieCategories { get; set; }

        [ObservableProperty]
        ObservableCollection<MovieRowViewModel> movieRowList;

        public HomePageViewModel(
            IGetMovieListUseCase getMovieListUseCase,
            IGetGenresUseCase getGenresUseCase)
        {
            this.getMovieListUseCase = getMovieListUseCase;
            this.getGenresUseCase = getGenresUseCase;

            MovieRowList = [];
            MovieCategories = Enum.GetValues(typeof(MovieCategory)).Cast<MovieCategory>().ToList();

            Task.Run(InitializeMovieRowList);
        }

        public async Task InitializeMovieRowList()
        {
            MovieRowList.Clear();
            
            // get genres into an array
            // initialize a task[] of List<Movie> for genres of size genres[]
            // loop the genre[] to invoke getMovieListUseCase on "genre", add it to the task 
            // loop the genre task arr to add movie list to MovieRowList

            List<Genre> genres = await getGenresUseCase.ExecuteAsync();

            // initialize tasks of getting movies based on genres
            Task<List<Movie>>[] movieListByGenreTasks = new Task<List<Movie>>[5];
            for (int i = 0; i < movieListByGenreTasks.Length; i++)
            {
                movieListByGenreTasks[i] = getMovieListUseCase.ExecuteAsync(genres[i].Id);
            }

            // initialize tasks of getting movies based on MovieCategory
            Task<List<Movie>>[] movieListByCategoryTasks = new Task<List<Movie>>[MovieCategories.Count];
            for (int i = 0; i < movieListByCategoryTasks.Length; i++)
            {
                movieListByCategoryTasks[i] = getMovieListUseCase.ExecuteAsync(MovieCategories[i]);
            }

            List<Movie>[] movieListByCategory = await Task.WhenAll(movieListByCategoryTasks);
            List<Movie>[] movieListsByGenre = await Task.WhenAll(movieListByGenreTasks);

            for (int i = 0; i < movieListByCategory.Length; i++)
            {
                if (movieListByCategory[i] is null)
                {
                    continue;
                }
                MovieRowList.Add(new MovieRowViewModel(MovieCategories[i], movieListByCategory[i]));
            }
            for (int i = 0; i < movieListsByGenre.Length; i++)
            {
                MovieRowList.Add(new MovieRowViewModel(genres[i].Name, movieListsByGenre[i]));
            }

        }

    }
}
