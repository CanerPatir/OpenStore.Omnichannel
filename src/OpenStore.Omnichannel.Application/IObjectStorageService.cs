namespace OpenStore.Omnichannel.Application;

public interface IObjectStorageService
{
    Task<(string host, string path)> Write(string fileName, byte[] content);
    Task Delete(string host, string path);
}