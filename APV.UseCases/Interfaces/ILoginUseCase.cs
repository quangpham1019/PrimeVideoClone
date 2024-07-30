using APV.CoreBusiness;

namespace APV.UseCases.Interfaces
{
    public interface ILoginUseCase
    {
        Task<UserInfo> ExecuteAsync(string username, string password);
    }
}