using Microsoft.Extensions.Logging;

#if ANDROID
using ResillentConstruction.Platforms.Android;
#endif

#if IOS
using ResillentConstruction.Platforms.iOS;
#endif

namespace ResillentConstruction
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
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    handlers.AddHandler<JustifiedLabel, JustifiedLabelHandler>();
#endif
#if IOS
                    handlers.AddHandler<JustifiedLabel, JustifiedLabelHandler>();
#endif
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
