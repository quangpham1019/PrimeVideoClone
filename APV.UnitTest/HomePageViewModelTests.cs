using APV._Plugins.WebAPI.Tmdb.Models;
using APV._UseCases.Interfaces;
using APV.UseCases.Interfaces;
using APV.ViewModels;
using FluentAssertions;
using NSubstitute;

namespace APV.UnitTest
{
    public class HomePageViewModelTests
    {
        [Fact]
        public void InitializeGetMovieListByGenreTasksTest()
        {
            // Arrange
            var getMovieListUseCase = Substitute.For<IGetMovieListUseCase>();
            var getGenresUseCase = Substitute.For<IGetGenresUseCase>();

            var viewModel = new HomePageViewModel(getMovieListUseCase, getGenresUseCase);

            List<Genre> genres = new List<Genre>
            {
                new Genre{Id=1, Name="Action"},
                new Genre{Id=2, Name="Comedy"},
                new Genre{Id=3, Name="Adventure"}
            };

            // Act
            var movieListByGenreTasks = viewModel.InitializeGetMovieListByGenreTasks(genres);

            // Assert
            movieListByGenreTasks.Length.Should().Be(3);
            movieListByGenreTasks[0].Should().NotBeNull();
            movieListByGenreTasks[1].Should().NotBeNull();
            movieListByGenreTasks[2].Should().NotBeNull();
        }
    }
}