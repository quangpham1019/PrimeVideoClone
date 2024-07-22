using APV.ViewModels;

namespace APV.UnitTest
{
    public class MovieDetailsViewModelTests
    {
        [Fact]
        public void MovieDetailsViewModelParameterlessConstructorTest()
        {
            // Arrange
            MovieDetailsViewModel viewModel = new MovieDetailsViewModel();

            // Act

            // Assert
            Assert.NotNull(viewModel);
            Assert.Null(viewModel.MovieDetails);
            Assert.Null(viewModel.SimilarMovies);
        }
    }
}
