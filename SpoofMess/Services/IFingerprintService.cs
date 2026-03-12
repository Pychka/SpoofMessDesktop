using CommonObjects.Requests.Files;

namespace SpoofMess.Services;

public interface IFingerprintService
{
    public Task<byte[]> GetFingerPrintL1(string filePath);
    public Task<byte[]> GetFingerPrintL2(string filePath);
    public Task<FingerprintExistL3> GetFingerPrintFull(string filePath);
    public Task<FingerprintExistL1L2> GetFingerPrintL1L2(string filePath);
}
