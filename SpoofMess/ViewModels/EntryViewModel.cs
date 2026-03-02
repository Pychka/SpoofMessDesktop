using CommonObjects.Responses;
using CommonObjects.Results;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpoofMess.Models;
using SpoofMess.Services;
using SpoofMess.Services.Api;
using System.Windows;

namespace SpoofMess.ViewModels;

internal partial class EntryViewModel(
     IEntryApiService entryApiService,
     INavigationService navigationService,
     IAuthService authService
    ) : ObservableObject
{
    private readonly IAuthService _authService = authService;
    private readonly IEntryApiService _entryApiService = entryApiService;
    private readonly INavigationService _navigationService = navigationService;


    public EntryModel Model { get; set; } = new();

    [RelayCommand]
    private void ChangeView()
    {

    }

    [RelayCommand]
    private async Task Entry()
    {
        Result<UserAuthorizeResponse> result = await _entryApiService.Enter(
                new()
                {
                    Login = Model.Login,
                    Password = Model.Password
                }
            );
        if (result.Success)
        {
            _authService.SetTokens(result.Body!);
            _navigationService.ShowMainView();
        }
        else
            MessageBox.Show(result.Error);
    }
}
