using SpoofMess.Models.API;

namespace SpoofMess.Services;

public interface INotificationService
{
    public void ShowToast(Notification notification);
    public void ShowMessageBox(Notification notification);
}
