using APV.CoreBusiness;
namespace APV.UseCases.PluginInterfaces
{
    public interface IUserDatabase
    {
        Task<UserInfo> GetUserInfo(string username, string password);
    }
}
