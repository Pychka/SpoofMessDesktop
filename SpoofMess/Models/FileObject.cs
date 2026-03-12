using CommunityToolkit.Mvvm.ComponentModel;

namespace SpoofMess.Models;

public partial class FileObject : ObservableObject
{
    [ObservableProperty]
    private byte[]? _token;
    [ObservableProperty]
    private string? _path;
    [ObservableProperty]
    private string? _name;
    [ObservableProperty]
    private long _size;
    [ObservableProperty]
    private short _extensionId;
}
