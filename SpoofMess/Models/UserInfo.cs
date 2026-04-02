using CommonObjects.Responses;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace SpoofMess.Models;

public partial class UserInfo : ObservableObject
{
    [JsonIgnore]
    [ObservableProperty]
    private string _password = string.Empty;

    public UserAuthorizeResponse? Authorize { get; set; }

    public SessionSettings SessionSettings { get; set; } = new();

    public User User { get; set; } = new();

    public void Update(UserInfo userInfo)
    {
        User = userInfo.User;
        Authorize = userInfo.Authorize;
        SessionSettings = userInfo.SessionSettings;
    }
}
