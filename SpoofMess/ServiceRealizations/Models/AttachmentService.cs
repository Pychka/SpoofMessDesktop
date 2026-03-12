using CommonObjects.Requests.Files;
using CommonObjects.Results;
using SpoofMess.Models;
using SpoofMess.Services;
using SpoofMess.Services.Api;
using SpoofMess.Services.Models;

namespace SpoofMess.ServiceRealizations.Models;

internal class AttachmentService(
    IFileService fileService,
    IFingerprintService fingerprintService,
    IFileApiService fileApiService) : IAttachmentService
{
    private readonly IFileService _fileService = fileService;
    private readonly IFileApiService _fileApiService = fileApiService;
    private readonly IFingerprintService _fingerprintService = fingerprintService;

    public Result Attach(MessageModel message)
    {
        Result<FileObject> file = _fileService.GetFileInfo();
        if (!file.Success)
            return Result.From(file);

        message.Attachments.Add(file.Body!);
        return Result.OkResult();
    }

    [Obsolete("Need remove file from server")]
    public void Unattach(FileObject file, MessageModel message)
    {
        message.Attachments.Remove(file);
    }

    public async Task<Result<byte[]>> SendAttachment(FileObject file)
    {
        if(file.Size < 50 * 1024 * 1024)
        {
            FingerprintExistL3 l3 = await _fingerprintService.GetFingerPrintFull(file.Path!);
            return await _fileApiService.ExistsL3(l3);
        }
        else
        {
            FingerprintExistL1L2 l1 = await _fingerprintService.GetFingerPrintL1L2(file.Path!);
            Result result = await _fileApiService.ExistsL1L2(l1);
            if (result.Success)
            {
                return await _fileApiService.Save(_fileService.GetStream(file.Path!));
            }
            FingerprintExistL3 l3 = await _fingerprintService.GetFingerPrintFull(file.Path!);
            Result<byte[]> resultL3 = await _fileApiService.ExistsL3(l3);
            if (!resultL3.Success)
            {
                return await _fileApiService.Save(_fileService.GetStream(file.Path!));
            }
            return resultL3;

        }
    }
}
