using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using APV.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace APV.ViewModels
{
    public partial class HomePageViewModel : ViewModel
    {
        public readonly IGetMovieListUseCase getMovieListUseCase;
        public readonly IGetGenresUseCase getGenresUseCase;

        MovieCategory[] MovieCategories { get; set; }

        [ObservableProperty]
        ObservableCollection<MovieRowViewModel> movieRowList;

        [ObservableProperty]
        ObservableCollection<Movie> movieCarousel;

        [ObservableProperty]
        DisplayInfo deviceDisplayInfo;

        public HomePageViewModel()
        {
        }

        public HomePageViewModel(
            IGetMovieListUseCase getMovieListUseCase,
            IGetGenresUseCase getGenresUseCase)
        {
            this.getMovieListUseCase = getMovieListUseCase;
            this.getGenresUseCase = getGenresUseCase;

            MovieRowList = [];
            MovieCarousel = [];
            MovieCategories = Enum.GetValues(typeof(MovieCategory)).Cast<MovieCategory>().ToArray();
            Task.Run(InitializeHomePageMovieData);

            DisplayInfo currentDeviceDisplayInfo = DeviceDisplay.Current.MainDisplayInfo;

            // TODO: add event handler to update deviceDisplayInfo when the app window is resized
#if WINDOWS
            deviceDisplayInfo = new DisplayInfo(
                Shell.Current.Window.Width,
                Shell.Current.Window.Height * .7,
                currentDeviceDisplayInfo.Density,
                currentDeviceDisplayInfo.Orientation,
                currentDeviceDisplayInfo.Rotation
                );
#endif

#if !WINDOWS
            deviceDisplayInfo = new DisplayInfo(
                Shell.Current.Window.Width,
                Shell.Current.Window.Height * .3,
                currentDeviceDisplayInfo.Density,
                currentDeviceDisplayInfo.Orientation,
                currentDeviceDisplayInfo.Rotation);
#endif
        }


        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public async Task InitializeHomePageMovieData()
        {
            MovieRowList.Clear();

            int numOfGenresToRender = 5;
            List<Genre> genres = await getGenresUseCase.ExecuteAsync();

            List<Movie>[] movieListByCategory = await Task.WhenAll(InitializeGetMovieListByCategoryTasks());
            List<Movie>[] movieListsByGenre = await Task.WhenAll(InitializeGetMovieListByGenreTasks(genres));

            AddMovieListByCategoryToMovieCarouselAndMovieRowList(movieListByCategory);

            (List<Movie>[] movieListsByGenreToRender, List<Genre> genresToRender) = ReduceNumOfMovieListsByGenreToRender(numOfGenresToRender, movieListsByGenre, genres);

            AddMovieListByGenreToMovieRowList(movieListsByGenreToRender, genresToRender);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numOfGenresToRender"></param>
        /// <param name="movieListsByGenre"></param>
        /// <param name="genres"></param>
        /// <returns></returns>
        private (List<Movie>[] movieListsByGenreToRender, List<Genre> genresToRender) ReduceNumOfMovieListsByGenreToRender(int numOfGenresToRender, List<Movie>[] movieListsByGenre, List<Genre> genres)
        {
            List<Movie>[] movieListsByGenreToRender = new List<Movie>[numOfGenresToRender];
            List<Genre> genresToRender = new List<Genre>();

            for (int i = 0; i < movieListsByGenreToRender.Length; i++)
            {
                movieListsByGenreToRender[i] = movieListsByGenre[i];
                genresToRender.Add(genres[i]);
            }

            return (movieListsByGenreToRender, genresToRender);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieListByCategory"></param>
        private void AddMovieListByCategoryToMovieCarouselAndMovieRowList(List<Movie>[] movieListByCategory)
        {
            for (int i = 0; i < movieListByCategory.Length; i++)
            {
                if (movieListByCategory[i] is null) continue;

                if (MovieCategories[i] == MovieCategory.Trending)
                {
                    MovieCarousel = new ObservableCollection<Movie>(movieListByCategory[i]);
                    continue;
                }

                MovieRowList.Add(new MovieRowViewModel(MovieCategories[i], movieListByCategory[i]));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<Movie>>[] InitializeGetMovieListByCategoryTasks()
        {
            Task<List<Movie>>[] movieListByCategoryTasks = new Task<List<Movie>>[MovieCategories.Length];
            for (int i = 0; i < movieListByCategoryTasks.Length; i++)
            {
                movieListByCategoryTasks[i] = getMovieListUseCase.ExecuteAsync(MovieCategories[i]);
            }

            return movieListByCategoryTasks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genres"></param>
        /// <returns></returns>
        public Task<List<Movie>>[] InitializeGetMovieListByGenreTasks(List<Genre> genres)
        {
            Task<List<Movie>>[] movieListByGenreTasks = new Task<List<Movie>>[genres.Count];
            for (int i = 0; i < movieListByGenreTasks.Length; i++)
            {
                movieListByGenreTasks[i] = getMovieListUseCase.ExecuteAsync(genres[i].Id);
            }

            return movieListByGenreTasks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieListByGenre"></param>
        /// <param name="genres"></param>
        public void AddMovieListByGenreToMovieRowList(List<Movie>[] movieListByGenre, List<Genre> genres)
        {
            for (int i = 0; i < movieListByGenre.Length; i++)
            {
                MovieRowList.Add(new MovieRowViewModel(genres[i].Name, movieListByGenre[i]));
            }
        }

        [RelayCommand]
        private async void GoToLoginPage()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
