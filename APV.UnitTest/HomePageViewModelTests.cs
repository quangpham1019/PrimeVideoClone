using APV._Plugins.WebAPI.Tmdb.Models;
using APV._UseCases.Interfaces;
using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using APV.ViewModels;
using FluentAssertions;
using NSubstitute;
using System.Collections.ObjectModel;

namespace APV.UnitTest
{
    public class HomePageViewModelTests
    {

        [Fact]
        public void HomePageViewModelDefaultConstructorTest()
        {
            // Arrange
            var viewModel = new HomePageViewModel();

            // Act
            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void InitializeGetMovieListByCategoryTasksTest()
        {
            // Arrange
            IGetMovieListUseCase getMovieListUseCase = Substitute.For<IGetMovieListUseCase>();
            IGetGenresUseCase getGenresUseCase = Substitute.For<IGetGenresUseCase>();

            HomePageViewModel viewModel = new HomePageViewModel(getMovieListUseCase, getGenresUseCase);

            MovieCategory[] movieCategories = Enum.GetValues(typeof(MovieCategory)).Cast<MovieCategory>().ToArray();
            int numOfMovieCategory = movieCategories.Length;

            for (int i = 0; i < numOfMovieCategory; i++)
            {
                MovieCategory curMovieCategory = movieCategories[i];
                getMovieListUseCase
                    .ExecuteAsync(curMovieCategory)
                    .Returns([
                        new Movie { Title=$"{curMovieCategory} 1" },
                        new Movie { Title=$"{curMovieCategory} 2" }
                        ]);
            }

            List<Movie> expectedMovieListOfLastCategory = new List<Movie>
            {
                new Movie { Title=$"{movieCategories[^1]} 1"},
                new Movie { Title=$"{movieCategories[^1]} 2"},
            };
            // Act
            Task<List<Movie>>[] movieListByCategoryTasks = viewModel.InitializeGetMovieListByCategoryTasks();

            // Assert
                // testSubject = movieListByCategoryTasks
                // Length of testSubject should be the count of MovieCategory enum
                // Each element of testSubject should not be null
                // Each element of testSubject is of type Task<List<Movie>>
                // Each Result of testSubject is of type List<Movie>

            Assert.Equal(numOfMovieCategory, movieListByCategoryTasks.Length);
            foreach (Task<List<Movie>> movieListByCategoryTask in movieListByCategoryTasks)
            {
                Assert.NotNull(movieListByCategoryTask);
                Assert.IsType<Task<List<Movie>>>(movieListByCategoryTask);
                Assert.IsType<List<Movie>>(movieListByCategoryTask.Result);
            }

            List<Movie> movieListByLastCategory = movieListByCategoryTasks[^1].Result;
            Assert.Equal(expectedMovieListOfLastCategory[^1].Title, movieListByLastCategory[^1].Title);
        }
        
        [Fact]
        public void InitializeGetMovieListByGenreTasksTest()
        {
            // Arrange
            var getMovieListUseCase = Substitute.For<IGetMovieListUseCase>();
            var getGenresUseCase = Substitute.For<IGetGenresUseCase>();

            List<Genre> genres = new()
            {
                new Genre{Id=1, Name="Action"},
                new Genre{Id=2, Name="Comedy"},
                new Genre{Id=3, Name="Adventure"}
            };
            getGenresUseCase.ExecuteAsync().Returns(Task.FromResult(genres));

            HomePageViewModel viewModel = new HomePageViewModel(getMovieListUseCase, getGenresUseCase);

            for (int i = 0; i < genres.Count; i++)
            {
                Genre curGenre = genres[i];

                getMovieListUseCase
                    .ExecuteAsync(curGenre.Id)
                    .Returns([
                        new Movie { Title=$"{curGenre.Name} 1", Genre_ids = [curGenre.Id] },
                        new Movie { Title=$"{curGenre.Name} 2", Genre_ids = [curGenre.Id] }
                        ]);
            }

            var expectedMovieListOfAdventureGenre = new List<Movie>()
            {
                new Movie { Title="Adventure 1", Genre_ids=[2]},
                new Movie { Title="Adventure 2", Genre_ids=[2]}
            };

            // Act
            Task<List<Movie>>[] movieListByGenreTasks = viewModel.InitializeGetMovieListByGenreTasks(genres);

            // Assert
                // testSubject = movieListByGenreTasks
                // The testSubject should have a length of genres.Count
                // Each element of testSubject should not be null
                // Each element of testSubject should be of type Task<List<Movie>>
                // Each Result of testSubject should be of type List<Movie>>
                // The title of last movie of Result of last element of testSubject
                    // is equal to title of last movie of expectedMovieListOfAdventureGenre

            Assert.Equal(genres.Count, movieListByGenreTasks.Length);
            foreach (var movieListbyGenreTask in movieListByGenreTasks)
            {
                Assert.NotNull(movieListbyGenreTask);
                Assert.IsType<Task<List<Movie>>>(movieListbyGenreTask);
                Assert.IsType<List<Movie>>(movieListbyGenreTask.Result);
            }

            List<Movie> lastMovieListByGenre = movieListByGenreTasks[^1].Result;
            Assert.Equal(lastMovieListByGenre[^1].Title, expectedMovieListOfAdventureGenre[^1].Title);
        }

        [Fact]
        public void AddMovieListByGenreToMovieRowListTest()
        {
            // Arrange
            List<Movie>[] movieList =
            {
                new List<Movie> {
                    new Movie { Id=1, Title="Action movie 1"},
                    new Movie { Id=2, Title="Action movie 2"},
                    new Movie { Id=3, Title="Action movie 3"}
                },
                new List<Movie> {
                    new Movie { Id=4, Title="Comedy movie 4"},
                    new Movie { Id=5, Title="Comedy movie 5"},
                    new Movie { Id=6, Title="Comedy movie 6"}
                },
                new List<Movie> {
                    new Movie { Id=7, Title="Adventure movie 7"},
                    new Movie { Id=8, Title="Adventure movie 8"},
                    new Movie { Id=9, Title="Adventure movie 9"}
                },
            };

            List<Genre> genres =
            [
                new Genre{Id=1, Name="Action"},
                new Genre{Id=2, Name="Comedy"},
                new Genre{Id=3, Name="Adventure"}
            ];

            HomePageViewModel viewModel = new HomePageViewModel()
            {
                MovieRowList = []
            };

            MovieRowViewModel expectedLastMovieRowViewModel = new MovieRowViewModel()
            {
                MovieList = new ObservableCollection<Movie> {
                    new Movie { Id=7, Title="Adventure movie 7"},
                    new Movie { Id=8, Title="Adventure movie 8"},
                    new Movie { Id=9, Title="Adventure movie 9"}
                },
                MovieRowHeading = "Adventure"
            };

            ObservableCollection<MovieRowViewModel> movieRowViewModelList = viewModel.MovieRowList;
            int originalMovieRowListCount = movieRowViewModelList.Count;

            // Act
            viewModel.AddMovieListByGenreToMovieRowList(movieList, genres);

            // Assert
                // movieRowViewModelList.Count should be increased by genres.Count after act
                // type of each element of movieRowViewModelList should be MovieRowViewModel
                // the title of the last movie of the last element of movieRowViewModelList should be movieList[^1][^1].Title
            Assert.Equal(originalMovieRowListCount + genres.Count, movieRowViewModelList.Count);
            Assert.True(movieRowViewModelList.All(x => x.GetType() == typeof(MovieRowViewModel)));

            MovieRowViewModel lastMovieRowViewModel = movieRowViewModelList[^1];
            Assert.Equal(lastMovieRowViewModel.MovieList[^1].Title, expectedLastMovieRowViewModel.MovieList[^1].Title);
        }
    }
}