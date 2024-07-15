using APV._Plugins.WebAPI.Tmdb.Models;

namespace APV._UseCases.Interfaces
{
    public interface IGetGenresUseCase
    {
        Task<List<Genre>> ExecuteAsync();
    }
}