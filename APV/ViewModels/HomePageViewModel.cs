using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.ViewModels
{
    public partial class HomePageViewModel : ViewModel
    {
        private readonly IGetMovieListUseCase getMovieListUseCase;
        MovieCategory[] MovieCategories { get; set; }

        [ObservableProperty]
        ObservableCollection<MovieRowViewModel> movieRowList;

        public HomePageViewModel(IGetMovieListUseCase getMovieListUseCase)
        {
            this.getMovieListUseCase = getMovieListUseCase;
            MovieRowList = [];
            MovieCategories =
            [
                MovieCategory.All,
                MovieCategory.ContinueWatching,
                MovieCategory.Popular,
                MovieCategory.Trending
            ];

            Task.Run(InitializeMovieRowList);
        }

        public async Task InitializeMovieRowList()
        {
            MovieRowList.Clear();

            Task<List<Movie>>[] movieTasks = new Task<List<Movie>>[MovieCategories.Length];

            for (int i = 0; i < MovieCategories.Length; i++)
            {
                movieTasks[i] = this.getMovieListUseCase.ExecuteAsync(MovieCategories[i]);
            }

            List<Movie>[] movieListsFromDB = await Task.WhenAll(movieTasks);

            for (int i = 0; i < MovieCategories.Length; i++)
            {
                MovieRowList.Add(new MovieRowViewModel(MovieCategories[i], movieListsFromDB[i]));
            }

        }

    }
}
