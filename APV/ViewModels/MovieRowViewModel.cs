using APV.CoreBusiness;
using APV.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace APV.ViewModels
{
    public partial class MovieRowViewModel : ViewModel
    {
        [ObservableProperty]
        ObservableCollection<Movie> movieList;

        [ObservableProperty]
        public MovieCategory movieCategory;

        public MovieRowViewModel(MovieCategory movieCategory, List<Movie> movieList)
        {
            MovieCategory = movieCategory;
            MovieList = new ObservableCollection<Movie>(movieList);
        }

        [RelayCommand]
        public async void ShowMovieDetails(Movie selectedMovie)
        {
            if (selectedMovie is not null)
            {
                var parameters = new Dictionary<string, object>
                {
                    ["MovieId"] = selectedMovie.Id
                };
                await Shell.Current.GoToAsync(nameof(MovieDetailsPage), false, parameters);
            }
        }
    }
}
