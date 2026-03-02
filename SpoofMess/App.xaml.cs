using AdditionalHelpers.ServiceRealizations;
using AdditionalHelpers.Services;
using Microsoft.Extensions.DependencyInjection;
using SpoofMess.ServiceRealizations;
using SpoofMess.ServiceRealizations.Api;
using SpoofMess.Services;
using SpoofMess.Services.Api;
using SpoofMess.ViewModels;
using SpoofMess.Views;
using System.Windows;

namespace SpoofMess;

public partial class App : Application
{
    private IServiceProvider? _serviceProvider;
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ServiceCollection services = new();

        services.AddTransient<AuthHandler>();
        services.AddHttpClient<IEntryApiService, EntryApiService>()
            .AddHttpMessageHandler<AuthHandler>();
        services.AddHttpClient<IChatApiService, ChatApiService>()
            .AddHttpMessageHandler<AuthHandler>();
        services.AddHttpClient<IChatUserApiService, ChatUserApiService>()
            .AddHttpMessageHandler<AuthHandler>();
        services.AddHttpClient<IMessageApiService, MessageApiService>()
            .AddHttpMessageHandler<AuthHandler>();

        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ISerializer, JsonSerializerService>();

        services.AddScoped<EntryViewModel>();
        services.AddSingleton<IMessageService, MessageService>();
        services.AddScoped<MainViewModel>();
        services.AddScoped<INotificationService, NotificationService>();

        services.AddSingleton<IAuthService, AuthService>();

        services.AddScoped<EntryWindow>();
        services.AddScoped<MainView>();

        _serviceProvider = services.BuildServiceProvider();
        INavigationService? navigationService = _serviceProvider.GetService<INavigationService>();
        navigationService!.ShowEntryView();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
    }
}
