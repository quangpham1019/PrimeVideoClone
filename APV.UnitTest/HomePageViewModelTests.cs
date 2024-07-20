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
            viewModel.Should().NotBeNull();
        }

        [Fact]
        public void InitializeGetMovieListByCategoryTasksTest()
        {
            // Arrange
            var getMovieListUseCase = Substitute.For<IGetMovieListUseCase>();
            var getGenresUseCase = Substitute.For<IGetGenresUseCase>();

            var viewModel = new HomePageViewModel(getMovieListUseCase, getGenresUseCase);
            var numOfMovieCategory = Enum.GetNames(typeof(MovieCategory)).Length;

            // Act
            var movieListByCategoryTasks = viewModel.InitializeGetMovieListByCategoryTasks();

            // Assert
            movieListByCategoryTasks.Length.Should().Be(numOfMovieCategory);
        }
        
        [Fact]
        public void InitializeGetMovieListByGenreTasksTest()
        {
            // Arrange
            var getMovieListUseCase = Substitute.For<IGetMovieListUseCase>();
            var getGenresUseCase = Substitute.For<IGetGenresUseCase>();
            List<Genre> genres = new List<Genre>
            {
                new Genre{Id=1, Name="Action"},
                new Genre{Id=2, Name="Comedy"},
                new Genre{Id=3, Name="Adventure"}
            };
            getGenresUseCase.ExecuteAsync().Returns(Task.FromResult(genres));

            HomePageViewModel viewModel = new HomePageViewModel(getMovieListUseCase, getGenresUseCase);

            var resultList = new List<List<Movie>>();
            for (int i = 0; i < genres.Count; i++)
            {
                Genre genre = genres[i];

                resultList.Add([
                    new Movie { Title=$"{genre.Name} 1", Genre_ids = new int[] { genre.Id} },
                    new Movie { Title=$"{genre.Name} 2", Genre_ids = new int[] { genre.Id} }
                    ]);

                getMovieListUseCase.ExecuteAsync(genre.Id).Returns(resultList[i]);
            }

            // Act
            Task<List<Movie>>[] movieListByGenreTasks = viewModel.InitializeGetMovieListByGenreTasks(genres);
            
            // Assert
            movieListByGenreTasks.Length.Should().Be(3);
            movieListByGenreTasks[0].GetType().Should().Be(typeof(Task<List<Movie>>));
            movieListByGenreTasks[0].Should().NotBeNull();
            movieListByGenreTasks[0].Result.Should().BeEquivalentTo(resultList[0]);
            //Assert.Equal(resultList[0], movieListByGenreTasks[0].Result);
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

            HomePageViewModel viewModel = new HomePageViewModel();
            viewModel.MovieRowList = [];
            ObservableCollection<MovieRowViewModel> movieRowList = viewModel.MovieRowList;
            int originalMovieRowListCount = movieRowList.Count;

            // Act
            viewModel.AddMovieListByGenreToMovieRowList(movieList, genres);

            // Assert
            movieRowList.Count.Should().Be(originalMovieRowListCount + genres.Count);

            MovieRowViewModel lastMovieRowViewModelAfterAdding = movieRowList[^1];
            lastMovieRowViewModelAfterAdding.GetType().Should().Be(typeof(MovieRowViewModel));
            lastMovieRowViewModelAfterAdding.MovieRowHeading.Should().Be(genres[^1].Name);
            lastMovieRowViewModelAfterAdding.MovieList[0].Title.Should().Be(movieList[^1][0].Title);
            lastMovieRowViewModelAfterAdding.MovieList[1].Title.Should().Be(movieList[^1][1].Title);
            lastMovieRowViewModelAfterAdding.MovieList[2].Title.Should().Be(movieList[^1][2].Title);
        }
    }
}