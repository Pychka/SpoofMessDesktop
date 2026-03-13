using CommonObjects.Results;
using SpoofMess.Models;
using System.IO;
using System.Net.Http;

namespace SpoofMess.Services;

public interface IFileService
{
    public string[]? GetFiles();

    public string? GetFile();

    public string[]? GetImages();

    public string? GetImage();
    public Task Save(Stream input, FileObject file);

    public Result<FileObject> GetFileInfo();

    public MultipartFormDataContent GetStream(string path);
}
