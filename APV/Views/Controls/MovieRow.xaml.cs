using APV._Plugins.WebAPI.Tmdb.Models;
using APV.CoreBusiness;
using APV.ViewModels;

namespace APV.Views.Controls;

public partial class MovieRow : ContentView
{
	public static readonly BindableProperty MovieRowHeadingProperty =
		BindableProperty.Create(nameof(MovieRowHeading), typeof(string), typeof(MovieRow), string.Empty);
    public static readonly BindableProperty MovieListProperty =
        BindableProperty.Create(nameof(MovieList), typeof(IEnumerable<Movie>), typeof(MovieRow), Enumerable.Empty<Movie>());


    public MovieRow()
	{
		InitializeComponent();
    }

	public string MovieRowHeading
	{
		get => (string)GetValue(MovieRowHeadingProperty);
		set => SetValue(MovieRowHeadingProperty, value);
	}

	public IEnumerable<Movie> MovieList
	{
		get => (IEnumerable<Movie>)GetValue(MovieListProperty);
		set => SetValue(MovieListProperty, value);
	}
}