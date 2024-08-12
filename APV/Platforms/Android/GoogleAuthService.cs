using Android.App;
using Android.Gms.Auth.Api.SignIn;
using APV.Services.Auth;
using APV.CoreBusiness;

namespace APV.Platforms.Android
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private Activity curActivity;
        private TaskCompletionSource<UserDTO> taskCompletionSource;
        private GoogleSignInOptions googleSignInOptions;
        private GoogleSignInClient googleSignInClient;
        private Task<UserDTO> GoogleAuthentication
        {
            get => taskCompletionSource.Task;
        }

        public GoogleAuthService()
        {
            // Get current activity
            curActivity = Platform.CurrentActivity;

            // Config Auth Option
            googleSignInOptions = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                            .RequestIdToken("892547391589-go8a8lip4vkcer0qrr33f2j0vsbm3r8q.apps.googleusercontent.com")
                            .RequestEmail()
                            .RequestId()
                            .RequestProfile()
                            .Build();

            // Get client
            googleSignInClient = GoogleSignIn.GetClient(curActivity, googleSignInOptions);

            MainActivity.ResultGoogleAuth += MainActivity_ResultGoogleAuth;
        }

        public Task<UserDTO> AuthenticateAsync()
        {
            taskCompletionSource = new TaskCompletionSource<UserDTO>();
            curActivity.StartActivityForResult(googleSignInClient.SignInIntent, 9001);

            return GoogleAuthentication;
        }

        private void MainActivity_ResultGoogleAuth(object sender, (bool Success, GoogleSignInAccount Account) e)
        {
            if (e.Success)
                taskCompletionSource.SetResult(new UserDTO
                {
                    Email = e.Account.Email,
                    FullName = e.Account.DisplayName,
                    Id = e.Account.Id,
                    UserName = e.Account.GivenName
                });
            else
                taskCompletionSource.SetException(new Exception("Error"));
        }

        public async Task<UserDTO> GetCurrentUserAsync()
        {
            try
            {
                var user = await googleSignInClient.SilentSignInAsync();
                return new UserDTO
                {
                    Email = user.Email,
                    FullName = $"{user.DisplayName}",
                    Id = user.Id,
                    UserName = user.GivenName
                };

            }
            catch (Exception)
            {
                throw new Exception("Error");
            }
        }

        public Task LogoutAsync() => googleSignInClient.SignOutAsync();
    }
}