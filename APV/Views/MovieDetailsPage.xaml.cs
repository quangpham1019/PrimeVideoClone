using APV.CoreBusiness;
using APV.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace APV.Views;

public partial class MovieDetailsPage : ContentPage
{
    private readonly MovieDetailsViewModel movieDetailsViewModel;

    public MovieDetailsPage(MovieDetailsViewModel movieDetailsViewModel)
	{
		InitializeComponent();
        this.movieDetailsViewModel = movieDetailsViewModel;
        BindingContext = movieDetailsViewModel;
    }
}