using APV._CoreBusiness;
using APV._UseCases.Interfaces;
using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace APV.ViewModels
{
    [QueryProperty(nameof(MovieId), "MovieId")]
    public partial class MovieDetailsViewModel : ViewModel
    {
        private readonly IGetMovieDetailsUseCase getMovieDetailsUseCase;
        private readonly IGetMovieListUseCase getMovieListUseCase;

        public int MovieId
        {
            set
            {
                InitializeMovieDetails(value);
            }
        }

        [ObservableProperty]
        public MovieDetails movieDetails;

        [ObservableProperty]
        public List<Movie> similarMovies;

        public MovieDetailsViewModel(
            IGetMovieDetailsUseCase getMovieDetailsUseCase,
            IGetMovieListUseCase getMovieListUseCase)
        {
            this.getMovieDetailsUseCase = getMovieDetailsUseCase;
            this.getMovieListUseCase = getMovieListUseCase;
            similarMovies = [];
        }

        private async void InitializeMovieDetails(int movieId)
        {
            MovieDetails = await this.getMovieDetailsUseCase.ExecuteAsync(movieId);
            SimilarMovies = await this.getMovieListUseCase.ExecuteAsync(movieId, "similar");
        }
    }
}
