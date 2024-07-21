using APV.ViewModels;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
