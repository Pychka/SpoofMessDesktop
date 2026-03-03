using CommonObjects.DTO;
using CommonObjects.Requests.Messages;
using SpoofMess.Models;

namespace SpoofMess.Setters;

public static class MessageSetter
{
    public static MessageModel Set(this MessageDTO message) =>
        new()
        {
            ChatId = message.ChatId,
            SentAt = message.SendAt,
            Text = message.Text,
            UserId = message.UserId,
            User = new()
            {
                Id = message.UserId,
                Name = message.UserName
            }
        };
    public static CreateMessageRequest Set(this MessageModel message) =>
        new()
        {
            ChatId = message.ChatId,
            Text = message.Text ?? "",
            Attachments = []
        };
}
