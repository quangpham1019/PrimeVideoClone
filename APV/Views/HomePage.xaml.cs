using APV.ViewModels;

namespace APV.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel homePageViewModel;
    private double PrevScrollY { get; set; } = 0;

    public HomePage(HomePageViewModel homePageViewModel)
    {
        InitializeComponent();
        BindingContext = homePageViewModel;
        this.homePageViewModel = homePageViewModel;
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {

        double curScrollY = e.ScrollY;
        bool menuIsVisible = label1.IsVisible;

        if (Math.Abs(curScrollY - PrevScrollY) > 20)
        {
            // if scrolling down, curScrollY > prevScrollY && menu is visible
                // hide menu, menu visible = false
            // if scrolling up && menu is not visible
                // show menu, menu visible = true
            if (curScrollY > PrevScrollY && menuIsVisible == true)
            {
                label1.IsVisible = false;
                label2.IsVisible = false;
            }
            else if (curScrollY < PrevScrollY && menuIsVisible == false)
            {
                label1.IsVisible = true;
                label2.IsVisible = true;
            }
        }

        PrevScrollY = e.ScrollY;
    }
}