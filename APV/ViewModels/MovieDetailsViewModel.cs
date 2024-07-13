using APV._UseCases.Interfaces;
using APV.CoreBusiness;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.ViewModels
{
    [QueryProperty(nameof(MovieId), "MovieId")]
    public partial class MovieDetailsViewModel : ViewModel
    {
        private readonly IGetMovieUseCase getMovieUseCase;

        public int MovieId
        {
            set
            {
                InitializeMovieDetails(value);
            }
        }

        [ObservableProperty]
        public Movie movie;

        public MovieDetailsViewModel(IGetMovieUseCase getMovieUseCase)
        {
            this.getMovieUseCase = getMovieUseCase;

        }

        private async void InitializeMovieDetails(int movieId)
        {
            Movie = await this.getMovieUseCase.ExecuteAsync(movieId);
        }
    }
}
