using CommonObjects.Requests.Files;
using CommonObjects.Results;
using SpoofMess.Models;
using SpoofMess.Services;
using SpoofMess.Services.Api;
using SpoofMess.Services.Models;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace SpoofMess.ServiceRealizations.Models;

internal class AttachmentService(
    IFileService fileService,
    IFingerprintService fingerprintService,
    IFileApiService fileApiService) : IAttachmentService
{
    private readonly IFileService _fileService = fileService;
    private readonly IFileApiService _fileApiService = fileApiService;
    private readonly IFingerprintService _fingerprintService = fingerprintService;

    public async Task UploadAttachments(MessageModel message)
    {
        await Parallel.ForEachAsync(message.Attachments, async (file, cancellationToken) =>
        {
            Result<Stream> result = await _fileApiService.Upload(file.Token);
            if (result.Success)
            {
                await _fileService.Save(result.Body!, file);
            }
            else
            {
                Debug.WriteLine("Пизда");
            }
        });
    }

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
        Result<byte[]> resultL3;
        if (file.Size < 50 * 1024 * 1024)
        {
            FingerprintExistL3 l3 = await _fingerprintService.GetFingerPrintFull(file.Path!);
            resultL3 = await _fileApiService.ExistsL3(l3);
            if (resultL3.Success)
                return resultL3;
            else if (resultL3.StatusCode == 404)
                return await Save(file);
            else return resultL3;
        }
        else
        {
            FingerprintExistL1L2 l1 = await _fingerprintService.GetFingerPrintL1L2(file.Path!);
            Result result = await _fileApiService.ExistsL1L2(l1);
            if (result.Success)
                return await Save(file);

            FingerprintExistL3 l3 = await _fingerprintService.GetFingerPrintFull(file.Path!);
            resultL3 = await _fileApiService.ExistsL3(l3);
            if (!resultL3.Success)
                return await Save(file);
            return resultL3;

        }
    }
    private async Task<Result<byte[]>> Save(FileObject file)
    {
        using MultipartFormDataContent form = _fileService.GetStream(file.Path!);
        return await _fileApiService.Save(form);
    }
}
