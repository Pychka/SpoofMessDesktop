using CommonObjects.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SpoofMess.Models;

public partial class Chat : ObservableObject
{
    public Guid Id { get; set; }

    [ObservableProperty]
    private string? _name;
    [ObservableProperty]
    private string _uniqueName = string.Empty;

    public int ChatTypeId { get; set; }

    public ObservableCollection<MessageModel> Messages { get; set; } = [];
    public ObservableCollection<PermissionResult> Rules { get; set; } = [];
}
