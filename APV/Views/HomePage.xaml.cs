using APV.CoreBusiness;
using APV.ViewModels;
using Microsoft.Maui.Animations;

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

    private async void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        double curScrollY = e.ScrollY;
        bool menuIsVisible = label1.IsVisible;

        if (Math.Abs(curScrollY - PrevScrollY) > 30)
        {
            // if scrolling down, curScrollY > prevScrollY && menu is visible
                // hide menu, menu visible = false
            // if scrolling up && menu is not visible
                // show menu, menu visible = true
            if (curScrollY > PrevScrollY && menuIsVisible == true)
            {
                label1.IsVisible = false;
            } else if (curScrollY < PrevScrollY && menuIsVisible == false)
            {
                label1.IsVisible = true;
            }
        }

        await Console.Out.WriteLineAsync($"PrevScrollY: {PrevScrollY}");
        PrevScrollY = e.ScrollY;
        await Console.Out.WriteLineAsync($"current event ScrollY: {e.ScrollY}");
        await Console.Out.WriteLineAsync("---------");



        //await label1.FadeTo(0, 1000, Easing.BounceOut);
        //if (label1.IsVisible)
        //{
        //    label1.IsVisible = false;
        //} else
        //{
        //    label1.IsVisible = true;
        //}
    }
}