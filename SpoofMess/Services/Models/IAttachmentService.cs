using CommonObjects.Results;
using SpoofMess.Models;

namespace SpoofMess.Services.Models;

public interface IAttachmentService
{
    public Result Attach(
        MessageModel message);
    public void Unattach(
        FileObject file,
        MessageModel message);

    public Task<Result<byte[]>> SendAttachment(FileObject file);
}
