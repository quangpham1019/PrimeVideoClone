using APV.ViewModels;

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