using APV.CoreBusiness;

namespace APV.UseCases.PluginInterfaces
{
    public interface IGoogleAuthService
    {
        Task<UserDTO> AuthenticateAsync();
        Task LogoutAsync();
        Task<UserDTO> GetCurrentUserAsync();
    }
}
