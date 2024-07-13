using APV.CoreBusiness;

namespace APV.Views.Controls;

public partial class MovieRow : ContentView
{
	public static readonly BindableProperty MovieCategoryProperty =
		BindableProperty.Create(nameof(MovieCategory), typeof(string), typeof(MovieRow), string.Empty);

    public static readonly BindableProperty MovieListProperty =
		BindableProperty.Create(nameof(MovieList), typeof(IEnumerable<Movie>), typeof(MovieRow), Enumerable.Empty<Movie>());

	public MovieRow()
	{
		InitializeComponent();
	}

	public string MovieCategory
	{
		get => (string)GetValue(MovieCategoryProperty);
		set => SetValue(MovieCategoryProperty, value);
	}

	public IEnumerable<Movie> MovieList
	{
		get => (IEnumerable<Movie>)GetValue(MovieListProperty);
		set => SetValue(MovieListProperty, value);
	}

}