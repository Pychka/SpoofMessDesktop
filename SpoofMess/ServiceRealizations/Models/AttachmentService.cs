using AdditionalHelpers.Services;
using CommonObjects.Requests.Attachments;
using CommonObjects.Requests.Files;
using CommonObjects.Results;
using SpoofFileParser.FileMetadata;
using SpoofMess.Enums;
using SpoofMess.Models;
using SpoofMess.Services;
using SpoofMess.Services.Api;
using SpoofMess.Services.Models;
using SpoofMess.ViewModels.FileViewModels;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace SpoofMess.ServiceRealizations.Models;

internal class AttachmentService(
    IFileService fileService,
    IFingerprintService fingerprintService,
    IFileApiService fileApiService,
    INavigationService navigationService,
    ISerializer serializer,
    UserInfo userInfo) : IAttachmentService
{
    private readonly INavigationService _navigationService = navigationService;
    private readonly UserInfo _userInfo = userInfo;
    private readonly ISerializer _serializer = serializer;
    private readonly IFileService _fileService = fileService;
    private readonly IFileApiService _fileApiService = fileApiService;
    private readonly IFingerprintService _fingerprintService = fingerprintService;

    public async Task UploadAttachments(MessageModel message, List<Attachment> attachments)
    {
        ConcurrentBag<FileObject> files = [];
        await Parallel.ForEachAsync(attachments, async (attachment, cancellationToken) =>
        {
            FileObject file = new()
            {
                Id = attachment.Id,
                Token = attachment.Token,
                Category = Enum.Parse<FileCategory>(attachment.Category, true),
                Name = attachment.OriginalFileName,
                Size = attachment.Size,
                Path = Path.Combine(_userInfo.SessionSettings.Directory, attachment.OriginalFileName),
                PrettySize = _fileService.ToPrettySize(attachment.Size),
                Metadata = _serializer.Deserialize<IFileMetadata>(attachment.Metadata ?? "")
            };
            await UploadAttachment(file, message, files);
        });
        foreach(var file in files)
            Application.Current.Dispatcher.Invoke(() =>
            {
                Add(file, message);
            });
        GC.Collect();
    }

    private async Task UploadAttachment(FileObject file, MessageModel message, ConcurrentBag<FileObject> files)
    {
        files.Add(file);

        try
        {
            await _fileService.Save(file);
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public FileViewModel GetViewModel(FileObject file) =>
        file.Category switch
        {
            FileCategory.Image => _navigationService.GetImageViewModel(file),
            FileCategory.Audio => _navigationService.GetMusicViewModel(file),
            _ => _navigationService.GetFileViewModel(file),
        };

    private void Add(FileObject file, MessageModel message)
    {
        FileViewModel fileView = GetViewModel(file);
        FileViewModel? vm = message.Attachments.FirstOrDefault(x => x.GetType() == fileView.GetType());
        if (vm is not null)
            vm.Files.Add(file);
        else
            message.Attachments.Add(fileView);
    }

    public Result Attach(MessageModel message)
    {
        Result<List<FileObject>> file = _fileService.GetFilesInfo();
        if (!file.Success)
            return Result.From(file);

        file.Body!.ForEach(x => Add(x, message));
        return Result.OkResult();
    }

    [Obsolete("Need remove file from server")]
    public void Unattach(FileObject file, MessageModel message)
    {
        FileViewModel fileView = GetViewModel(file);
        var vm = message.Attachments.FirstOrDefault(x => x.GetType() == fileView.GetType());
        if (vm is not null)
        {
            FileObject? model = vm.Files.FirstOrDefault(x => x == file);
            if (model is not null)
                Application.Current.Dispatcher.Invoke(() =>
                {
                    vm.Files.Remove(model);
                });
        }
    }

    public async Task<Result<List<Attachment>>> SendAttachments(MessageModel message, CancellationToken token = default)
    {
        ParallelOptions options = new()
        {
            CancellationToken = new(),
            MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount / 2)
        };
        ConcurrentBag<Attachment> attachments = [];
        await Parallel.ForEachAsync(message.Attachments.SelectMany(x => x.Files), options, async (file, ct) =>
        {
            Result<byte[]> accessFileToken = await SendAttachment(file, token);
            if (accessFileToken.Success)
                attachments.Add(new(null, accessFileToken.Body!, file.Name!, string.Empty, string.Empty, file.Size));
        });
        return Result<List<Attachment>>.OkResult(attachments.ToList());
    }

    public async Task<Result<byte[]>> SendAttachment(FileObject file, CancellationToken token = default)
    {
        Result<byte[]> resultL3;
        if (file.Size < 50 * 1024 * 1024)
        {
            FingerprintExistL3 l3 = await _fingerprintService.GetFingerPrintFull(file.Path!, token);
            resultL3 = await _fileApiService.ExistsL3(l3, token);
            if (resultL3.Success)
                return resultL3;
            else if (resultL3.StatusCode == 404)
                return await Save(file, token);
            else return resultL3;
        }
        else
        {
            FingerprintExistL1L2 l1 = _fingerprintService.GetFingerPrintL1L2(file.Path!);
            Result result = await _fileApiService.ExistsL1L2(l1, token);
            if (result.Success)
                return await Save(file, token);

            FingerprintExistL3 l3 = await _fingerprintService.GetFingerPrintFull(file.Path!, token);
            resultL3 = await _fileApiService.ExistsL3(l3, token);
            if (!resultL3.Success)
                return await Save(file, token);
            return resultL3;

        }
    }
    private async Task<Result<byte[]>> Save(FileObject file, CancellationToken token = default)
    {
        using MultipartFormDataContent form = _fileService.GetStream(file.Path!);
        return await _fileApiService.Save(form, token);
    }
}
