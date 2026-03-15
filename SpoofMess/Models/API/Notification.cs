using SpoofMess.Enums;

namespace SpoofMess.Models.API;

public class Notification
{
    public string? Text
    {
        get => field ?? "Undefined";
        set => field = value;
    }

    public NotificationType Type { get; set; }
}
