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

        [ObservableProperty]
        ObservableCollection<MovieRowViewModel> movieRowList;

        public HomePageViewModel(IGetMovieListUseCase getMovieListUseCase)
        {
            this.getMovieListUseCase = getMovieListUseCase;
            MovieRowList = new ObservableCollection<MovieRowViewModel>();
            Task.Run(InitializeMovieRowList);
        }

        public async Task InitializeMovieRowList()
        {
            MovieRowList.Clear();

            MovieCategory[] movieCategoryArr =
            {
                MovieCategory.All,
                MovieCategory.ContinueWatching,
                MovieCategory.Popular,
                MovieCategory.Trending
            };

            Task<List<Movie>>[] movieTasks = new Task<List<Movie>>[movieCategoryArr.Length];

            for (int i = 0; i < movieCategoryArr.Length; i++)
            {
                movieTasks[i] = this.getMovieListUseCase.ExecuteAsync(movieCategoryArr[i]);
            }

            List<Movie>[] movieListsFromDB = await Task.WhenAll(movieTasks);

            for (int i = 0; i < movieCategoryArr.Length; i++)
            {
                MovieRowList.Add(new MovieRowViewModel(movieCategoryArr[i], movieListsFromDB[i]));
            }

        }

    }
}
