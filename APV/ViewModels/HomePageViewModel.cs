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
        ObservableCollection<Movie> movieList;

        [ObservableProperty]
        public MovieCategory movieCategory;

        public HomePageViewModel(IGetMovieListUseCase getMovieListUseCase)
        {
            this.getMovieListUseCase = getMovieListUseCase;
            MovieList = new ObservableCollection<Movie>();
            _ = Task.Run(async () => await InitializeMovieList(MovieCategory));
        }

        public async Task InitializeMovieList(MovieCategory movieCategory = default)
        {
            List<Movie> moviesFromDB = await this.getMovieListUseCase.ExecuteAsync(movieCategory);
            MovieList = new ObservableCollection<Movie>(moviesFromDB);
        }
    }
}
