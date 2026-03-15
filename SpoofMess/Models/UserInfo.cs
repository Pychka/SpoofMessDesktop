using CommonObjects.Responses;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SpoofMess.Models;

public partial class UserInfo : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;
    [ObservableProperty]
    private string _login = string.Empty;
    [ObservableProperty]
    private string _password = string.Empty;
    private UserAuthorizeResponse? Authorize { get; set; }
    public SessionSettings SessionSettings { get; set; } = new();
}
