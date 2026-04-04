using CommunityToolkit.Mvvm.ComponentModel;
using SpoofMess.Enums;

namespace SpoofMess.Models.API;

public partial class Notification : ObservableObject
{
    public string? Text
    {
        get => field ?? "Undefined";
        set => field = value;
    }

    public NotificationType Type { get; set; }

    [ObservableProperty]
    private double _opacity = 1;

    public int Time => Type switch { Enums.NotificationType.Error => 4000, Enums.NotificationType.Info => 2000, _ => 3000 };
}
