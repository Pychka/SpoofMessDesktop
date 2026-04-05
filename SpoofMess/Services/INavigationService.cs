using CommunityToolkit.Mvvm.ComponentModel;
using SpoofMess.Models;
using SpoofMess.ViewModels;
using SpoofMess.ViewModels.FileViewModels;

namespace SpoofMess.Services;

public interface INavigationService
{
    public void ShowCentralViewWithMain();
    public void ShowCentralViewWithAuthorization();
    public void ShowCentralView();
    public void ShowMainView();
    public void ShowRegistrationView();
    public void ShowAuthorizationView();
    public ImageViewModel GetImageViewModel(FileObject file);
    public MusicViewModel GetMusicViewModel(FileObject file);
    public FileViewModel GetFileViewModel(FileObject file);
    public SettingsViewModel GetSettingsViewModel();
    public ProfileViewModel GetProfileViewModel();
    public CreateGroupViewModel GetCreateGroupViewModel();
}
