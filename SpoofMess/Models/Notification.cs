using SpoofMess.Enums;

namespace SpoofMess.Models;

public class Notification
{
    public string Text { get; set; } = string.Empty;

    public NotificationType Type { get; set; }
}
