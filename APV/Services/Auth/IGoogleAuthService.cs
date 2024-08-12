using APV.CoreBusiness;

namespace APV.Services.Auth
{
    public interface IGoogleAuthService
    {
        Task<UserDTO> AuthenticateAsync();
        Task LogoutAsync();
        Task<UserDTO> GetCurrentUserAsync();
    }
}
