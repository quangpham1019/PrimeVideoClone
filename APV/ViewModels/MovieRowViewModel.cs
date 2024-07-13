using APV.CoreBusiness;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
