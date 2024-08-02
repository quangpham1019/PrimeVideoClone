using Android.App;
using Android.Gms.Auth.Api.SignIn;
using APV.Services.Auth;
using APV.CoreBusiness;

namespace APV.Platforms.Android
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private Activity _activity;
        private TaskCompletionSource<UserDTO> _taskCompletionSource;
        private GoogleSignInOptions _gso;
        private GoogleSignInClient _googleSignInClient;
        private Task<UserDTO> GoogleAuthentication
        {
            get => _taskCompletionSource.Task;
        }

        public GoogleAuthService()
        {
            // Get current activity
            _activity = Platform.CurrentActivity;

            // Config Auth Option
            _gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                            .RequestIdToken("892547391589-go8a8lip4vkcer0qrr33f2j0vsbm3r8q.apps.googleusercontent.com")
                            .RequestEmail()
                            .RequestId()
                            .RequestProfile()
                            .Build();

            // Get client
            _googleSignInClient = GoogleSignIn.GetClient(_activity, _gso);

            MainActivity.ResultGoogleAuth += MainActivity_ResultGoogleAuth;
        }

        public Task<UserDTO> AuthenticateAsync()
        {
            _taskCompletionSource = new TaskCompletionSource<UserDTO>();
            _activity.StartActivityForResult(_googleSignInClient.SignInIntent, 9001);

            return GoogleAuthentication;
        }

        private void MainActivity_ResultGoogleAuth(object sender, (bool Success, GoogleSignInAccount Account) e)
        {
            if (e.Success)
                // Set result of Task
                _taskCompletionSource.SetResult(new UserDTO
                {
                    Email = e.Account.Email,
                    FullName = e.Account.DisplayName,
                    Id = e.Account.Id,
                    UserName = e.Account.GivenName
                });
            else
                // Set Exception
                _taskCompletionSource.SetException(new Exception("Error"));
        }

        public async Task<UserDTO> GetCurrentUserAsync()
        {
            try
            {
                var user = await _googleSignInClient.SilentSignInAsync();
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

        public Task LogoutAsync() => _googleSignInClient.SignOutAsync();
    }
}