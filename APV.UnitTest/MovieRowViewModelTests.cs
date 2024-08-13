using APV.ViewModels;
using Assert = Xunit.Assert;

namespace APV.UnitTest
{
    public class MovieRowViewModelTests
    {
        [Fact]
        public void MovieRowViewModelParameterlessConstructorTest()
        {
            // Arrange
            MovieRowViewModel viewModel = new MovieRowViewModel();

            // Act

            // Assert
            Assert.NotNull(viewModel);
            Assert.Null(viewModel.MovieList);
            Assert.Null(viewModel.MovieRowHeading);
        }
    }
}
