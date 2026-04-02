using AdditionalHelpers.Services;
using CommonObjects.Requests.Avatars;
using CommonObjects.Results;
using SpoofMess.Services.Api;
using System.Net.Http;

namespace SpoofMess.ServiceRealizations.Api;

internal class UserAvatarApiService(
    HttpClient client,
    ISerializer serializer) : ApiService(
        client,
        serializer), IUserAvatarApiService
{
    protected override string BaseUrl => "https://localhost:7082/api/v2/UserAvatar";

    public async Task<Result> Set(SesUserAvatarRequest request, CancellationToken token = default)
    {
        return await PostAsync($"/Set", request, token);
    }
}
