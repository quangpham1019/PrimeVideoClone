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
        string movieRowHeading;

        public MovieRowViewModel()
        {
        }

        public MovieRowViewModel(List<Movie> movieList)
        {
            MovieList = new ObservableCollection<Movie>(movieList);
        }

        public MovieRowViewModel(MovieCategory movieCategory, List<Movie> movieList) : this(movieList)
        {
            MovieRowHeading = movieCategory.ToString();
        }
        public MovieRowViewModel(string genreName, List<Movie> movieList) : this(movieList)
        {
            MovieRowHeading = genreName;
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
