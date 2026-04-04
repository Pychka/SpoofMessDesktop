using SpoofMess.Models.API;
using SpoofMess.Services;
using SpoofMess.ViewModels;
using System.Windows;

namespace SpoofMess.ServiceRealizations;

public class NotificationService(CentralViewModel centralViewModel) : INotificationService
{
    private readonly CentralViewModel _centralViewModel = centralViewModel; 
    private static readonly SemaphoreSlim _semaphore = new(1, 1);
    public void ShowMessageBox(Notification notification)
    {
        MessageBox.Show(
            notification.Text, 
            notification.Type.ToString(),
            MessageBoxButton.OK
            );
    }

    public async void ShowToast(Notification notification)
    {
        await _semaphore.WaitAsync();
        try
        {
            _centralViewModel.Notification = notification;
            await Task.Delay(notification.Time / 4);
            for (int i = 0; i < 30; i++)
            {
                notification.Opacity -= 0.03;
                await Task.Delay(notification.Time / 40);
            }
            _centralViewModel.Notification = null;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
