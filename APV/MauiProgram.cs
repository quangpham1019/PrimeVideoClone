using APV._Plugins.InMemory;
using APV._UseCases;
using APV._UseCases.Interfaces;
using APV.UseCases;
using APV.UseCases.Interfaces;
using APV.UseCases.PluginInterfaces;
using APV.ViewModels;
using APV.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

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
            mauiAppBuilder.Services.AddSingleton<IMovieRepository, APVInMemoryRepository>();
            mauiAppBuilder.Services.AddSingleton<IGetMovieListUseCase, GetMovieListUseCase>();
            mauiAppBuilder.Services.AddSingleton<IGetMovieUseCase, GetMovieUseCase>();

            return mauiAppBuilder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<HomePageViewModel>();
            mauiAppBuilder.Services.AddSingleton<MovieDetailsViewModel>();

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
