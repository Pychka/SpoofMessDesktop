using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;

namespace SpoofMess.Models;

public partial class SessionSettings : ObservableObject
{
    [ObservableProperty]
    private string _directory = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Downloads");
}
