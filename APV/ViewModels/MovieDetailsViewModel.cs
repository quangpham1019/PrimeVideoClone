using APV.CoreBusiness;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV.ViewModels
{
    [QueryProperty("Movie", nameof(Movie))]
    public partial class MovieDetailsViewModel : ViewModel
    {
        public MovieDetailsViewModel()
        {
            
        }

        [ObservableProperty]
        public Movie movie;
    }
}
