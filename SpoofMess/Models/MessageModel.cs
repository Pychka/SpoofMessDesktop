using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SpoofMess.Models;

public partial class MessageModel : ObservableObject
{
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }

    [ObservableProperty]
    private string? _text;
    [ObservableProperty]
    private DateTime _sentAt;
    [ObservableProperty]
    private User? user;
    [ObservableProperty]
    private Chat? chat;

    public ObservableCollection<FileObject> Attachments { get; set; } = [];
}
