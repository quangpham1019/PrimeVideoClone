﻿using APV.CoreBusiness;

namespace APV.UseCases.Interfaces
{
    public interface IGetMovieListUseCase
    {
        Task<List<Movie>> ExecuteAsync(MovieCategory movieCategory = default);
        Task<List<Movie>> ExecuteAsync(int genreId);
        Task<List<Movie>> ExecuteAsync(int movieId, string filter);
    }
}