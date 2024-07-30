using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using APV.UseCases.Interfaces;
using APV.CoreBusiness;
using APV.Views;

namespace APV.ViewModels
{
    public partial class LoginPageViewModel : ViewModel
    {
        private readonly ILoginUseCase loginUseCase;
        [ObservableProperty]
        private string username;
        [ObservableProperty]
        private string password;

        public LoginPageViewModel(ILoginUseCase loginUseCase)
        {
            this.loginUseCase = loginUseCase;
        }

        [RelayCommand]
        public async void Login()
        {
            // Check for username and password in db
            // display error if no match
            // log in if match
                // set user info in App

            UserInfo userInfo = await loginUseCase.ExecuteAsync(username, password);

            if (userInfo.Username == null)
            {
                // display error / go to LoginFailed page
                await Shell.Current.GoToAsync(nameof(LoginFailedPage));
            }
            else
            {
                // log userinfo into Preferences
                // go to home/ LoginSuccessPage
                await Shell.Current.GoToAsync(nameof(LoginSuccessPage));
            }
        }
    }
}
