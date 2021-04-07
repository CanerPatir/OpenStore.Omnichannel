using System;
using System.Collections.Generic;
using System.IO;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.ProductContext.Command
{
    public record AddMediaToProductCommand(string ProductId, Stream Stream, string FileName) : ICommand, IDisposable
    {
        public void Dispose() => Stream?.Dispose();
    }
    
    public record CreateProductCommand(
        Guid CategoryId,
        Guid BrandId,
        string Name,
        string Description) : ICommand<string>;
    
    public record CreateVariantCommand(string ProductId, IEnumerable<SetVariantAttributeCommand> SetVariantAttributeCommands) : ICommand<string>;

    public record RemoveSpecificationAttributeCommand(string Id, Guid AttributeId) : ICommand;
    

    public record SetSpecificationAttributeToProductCommand(string ProductId, Guid AttributeId, Guid? AttributeValueId, string CustomValue) : ICommand;

    public abstract record TagCommand(string Id, Guid TagId) : ICommand;

    public record SetTagToProductCommand(string Id, Guid TagId) : TagCommand(Id, TagId);

    public record RemoveTagFromProductCommand(string Id, Guid TagId) : TagCommand(Id, TagId);

    public record SetVariantAttributeCommand(Guid AttributeId, Guid? AttributeValueId, string CustomValue) : ICommand;

}