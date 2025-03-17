

using CandyMAUI.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using CandyMAUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CandyMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();

            // Pozivanje metode za dodavanje servisa
            AddCandyServices(builder.Services);

#if DEBUG
            builder.Logging.AddDebug();
#endif
            AddCandyServices (builder.Services);

            return builder.Build();
        }

        private static IServiceCollection AddCandyServices(IServiceCollection services)
        {
            services.AddSingleton<CandyServices>();
            services.AddSingleton<HomePage>()
                .AddSingleton<HomeViewModel>();
            services.AddSingletonWithShellRoute<HomePage, HomeViewModel>(nameof(HomePage));
            services.AddTransientWithShellRoute<AllCandiesPage,AllCandiesViewModel>(nameof(AllCandiesPage));

            
            services.AddTransientWithShellRoute<DetailPage, DetailsViewModel>(nameof(DetailPage));

          
            services.AddSingleton<CartViewModel>();
            services.AddTransient<CartPage>();
            return services;
        }
    }
}