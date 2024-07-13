using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace APV.ViewModels
{
    public partial class HomePageViewModel : ViewModel
    {
        private readonly IGetMovieListUseCase getMovieListUseCase;
        List<MovieCategory> MovieCategories { get; set; }

        [ObservableProperty]
        ObservableCollection<MovieRowViewModel> movieRowList;

        public HomePageViewModel(IGetMovieListUseCase getMovieListUseCase)
        {
            this.getMovieListUseCase = getMovieListUseCase;
            MovieRowList = [];
            MovieCategories = Enum.GetValues(typeof(MovieCategory)).Cast<MovieCategory>().ToList();

            Task.Run(InitializeMovieRowList);
        }

        public async Task InitializeMovieRowList()
        {
            MovieRowList.Clear();

            Task<List<Movie>>[] movieTasks = new Task<List<Movie>>[MovieCategories.Count];

            for (int i = 0; i < movieTasks.Length; i++)
            {
                movieTasks[i] = this.getMovieListUseCase.ExecuteAsync(MovieCategories[i]);
            }

            List<Movie>[] movieListsFromDB = await Task.WhenAll(movieTasks);

            for (int i = 0; i < movieListsFromDB.Length; i++)
            {
                MovieRowList.Add(new MovieRowViewModel(MovieCategories[i], movieListsFromDB[i]));
            }

        }

    }
}
