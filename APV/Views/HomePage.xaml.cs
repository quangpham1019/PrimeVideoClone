using APV.CoreBusiness;
using APV.ViewModels;
using Java.Lang;

namespace APV.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel homePageViewModel;

    public HomePage(HomePageViewModel homePageViewModel)
    {
        InitializeComponent();
        BindingContext = homePageViewModel;
        this.homePageViewModel = homePageViewModel;
    }
}