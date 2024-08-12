using APV.UseCases;
using APV.UseCases.Interfaces;
using APV.UseCases.PluginInterfaces;
using APV.ViewModels;
using APV.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using APV.Plugins.DataStore.WebAPI.Tmdb;
using APV.Services.Auth;

namespace APV
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            //mauiAppBuilder.Services.AddSingleton<IMovieRepository, APVInMemoryRepository>();
            mauiAppBuilder.Services.AddSingleton<IMovieRepository, APVTmdbRepository>();
            mauiAppBuilder.Services.AddSingleton<IGetMovieListUseCase, GetMovieListUseCase>();
            mauiAppBuilder.Services.AddSingleton<IGetMovieDetailsUseCase, GetMovieDetailsUseCase>();
            mauiAppBuilder.Services.AddSingleton<IGetGenresUseCase, GetGenresUseCase>();
            mauiAppBuilder.Services.AddSingleton<IGoogleAuthService, Platforms.Android.GoogleAuthService>();

            mauiAppBuilder.Services.AddHttpClient(APVTmdbRepository.TmdbHttpClientName,
    httpClient => httpClient.BaseAddress = new Uri("https://api.themoviedb.org"));

            return mauiAppBuilder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<HomePageViewModel>();
            mauiAppBuilder.Services.AddSingleton<MovieDetailsViewModel>();
            mauiAppBuilder.Services.AddSingleton<MovieRowViewModel>();

            return mauiAppBuilder;
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<HomePage>();
            mauiAppBuilder.Services.AddSingleton<StorePage>();
            mauiAppBuilder.Services.AddSingleton<DownloadsPage>();
            mauiAppBuilder.Services.AddSingleton<SearchPage>();
            mauiAppBuilder.Services.AddSingleton<MovieDetailsPage>();

            return mauiAppBuilder;
        }
    }
}
