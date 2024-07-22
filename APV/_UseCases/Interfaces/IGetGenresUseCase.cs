using APV.CoreBusiness;

namespace APV._UseCases.Interfaces
{
    public interface IGetGenresUseCase
    {
        Task<List<Genre>> ExecuteAsync();
    }
}