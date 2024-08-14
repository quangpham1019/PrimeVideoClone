using APV.CoreBusiness;
using APV.UseCases.PluginInterfaces;

namespace APV.UseCases
{
    public class GoogleAuthServiceImpl : IGoogleAuthService
    {
        public GoogleAuthServiceImpl()
        {
        }

        public Task<UserDTO> AuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync() => throw new NotImplementedException();
    }
}
