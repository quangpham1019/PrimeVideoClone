using APV._CoreBusiness;
using APV._UseCases.Interfaces;
using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using APV.ViewModels;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
