using CommunityToolkit.Mvvm.ComponentModel;

namespace SpoofMess.Models;

public partial class User : ObservableObject
{
    public Guid Id { get; set; }

    [ObservableProperty]
    private string? _name;
    [ObservableProperty]
    private string _login = string.Empty;
}
