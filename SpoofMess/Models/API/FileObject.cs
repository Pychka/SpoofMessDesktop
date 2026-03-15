using CommunityToolkit.Mvvm.ComponentModel;
using SpoofMess.Enums;
using System.Text.Json.Serialization;

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
    public FileCategory Category { get; set; }
    [ObservableProperty]
    public byte[] _thumbnail = []; 
    [ObservableProperty]
    [JsonIgnore]
    private string _prettySize = string.Empty;
}
