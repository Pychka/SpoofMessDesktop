using CommonObjects.Requests.Files;
using CommonObjects.Results;
using System.Net.Http;

namespace SpoofMess.Services.Api;

public interface IFileApiService
{
    public Task<Result> ExistsL1L2(FingerprintExistL1L2 l1L2);

    public Task<Result<byte[]>> ExistsL3(FingerprintExistL3 l3);
    public Task<Result<byte[]>> Save(MultipartFormDataContent content);
}
