using APV.CoreBusiness;

namespace APV.UseCases.Interfaces
{
    public interface IGetGenresUseCase
    {
        Task<List<Genre>> ExecuteAsync();
    }
}