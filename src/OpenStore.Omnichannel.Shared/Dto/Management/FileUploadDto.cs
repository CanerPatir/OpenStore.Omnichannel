namespace OpenStore.Omnichannel.Shared.Dto.Management;

public record FileUploadDto(string FileName, string Type, long? Size, int Position, byte[] FileContent);