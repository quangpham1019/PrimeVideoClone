using APV.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace APV.Views;

public partial class MovieDetailsPage : ContentPage
{
    private readonly MovieDetailsViewModel movieDetailsViewModel;

    Dictionary<Label, List<Border>> tabNameTabContentDict = new Dictionary<Label, List<Border>>();
    public MovieDetailsPage(MovieDetailsViewModel movieDetailsViewModel)
	{
		InitializeComponent();
        this.movieDetailsViewModel = movieDetailsViewModel;
        BindingContext = movieDetailsViewModel;
        InitializeTabNameTabContentDict();

    }

    private void InitializeTabNameTabContentDict()
    {
        tabNameTabContentDict.Add(relatedTabLabel, new() { related, relatedTabIndicator });
        tabNameTabContentDict.Add(moreDetailsTabLabel, new() { moreDetails, moreDetailsTabIndicator });
    }

    private void TabLabel_Tapped(object sender, TappedEventArgs e)
    {

        foreach (KeyValuePair<Label, List<Border>> keyValuePair in tabNameTabContentDict)
        {
            if (sender == keyValuePair.Key)
            {
                foreach(Border border in keyValuePair.Value)
                {
                    border.IsVisible = true;
                }

                continue;
            }

            foreach(Border border in keyValuePair.Value)
            {
                border.IsVisible = false;
            }
        }
    }
}