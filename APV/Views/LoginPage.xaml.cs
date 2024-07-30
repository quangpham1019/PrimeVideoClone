using APV.ViewModels;

namespace APV.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginPageViewModel loginPageViewModel;

    public LoginPage(LoginPageViewModel loginPageViewModel)
	{
		InitializeComponent();
        this.loginPageViewModel = loginPageViewModel;
        BindingContext = loginPageViewModel;
    }
}