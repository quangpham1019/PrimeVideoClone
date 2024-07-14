using APV._CoreBusiness;
using APV._UseCases.Interfaces;
using APV.CoreBusiness;
using CommunityToolkit.Mvvm.ComponentModel;

namespace APV.ViewModels
{
    [QueryProperty(nameof(MovieId), "MovieId")]
    public partial class MovieDetailsViewModel : ViewModel
    {
        private readonly IGetMovieDetailsUseCase getMovieUseCase;

        public int MovieId
        {
            set
            {
                InitializeMovieDetails(value);
            }
        }

        [ObservableProperty]
        public MovieDetails movieDetails;

        public MovieDetailsViewModel(IGetMovieDetailsUseCase getMovieUseCase)
        {
            this.getMovieUseCase = getMovieUseCase;

        }

        private async void InitializeMovieDetails(int movieId)
        {
            MovieDetails = await this.getMovieUseCase.ExecuteAsync(movieId);
        }
    }
}
