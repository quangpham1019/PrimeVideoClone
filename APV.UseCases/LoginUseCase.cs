using APV.CoreBusiness;
using APV.UseCases.Interfaces;
using APV.UseCases.PluginInterfaces;

namespace APV.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserDatabase userDatabase;
        public LoginUseCase(IUserDatabase userDatabase)
        {
            this.userDatabase = userDatabase;
        }

        public async Task<UserInfo> ExecuteAsync(string username, string password)
        {
            return await this.userDatabase.GetUserInfo(username, password);
        }
    }
}
