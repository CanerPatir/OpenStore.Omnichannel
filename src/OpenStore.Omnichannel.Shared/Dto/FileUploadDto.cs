using System;

namespace OpenStore.Omnichannel.Shared.Dto;

public record FileUploadDto(string FileName, string Type, long? Size, int Position, byte[] FileContent);