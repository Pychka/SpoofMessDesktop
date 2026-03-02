using CommunityToolkit.Mvvm.ComponentModel;

namespace SpoofMess.Models;

public partial class EntryModel : ObservableObject
{
    [ObservableProperty]
    private string _login = string.Empty;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;
}
