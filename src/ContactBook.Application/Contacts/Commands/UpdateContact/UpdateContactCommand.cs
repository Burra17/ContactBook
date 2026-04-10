using ContactBook.Application.Contacts.Dtos;
using ContactBook.Domain.Shared;
using MediatR;
using System.Text.Json.Serialization;

namespace ContactBook.Application.Contacts.Commands.UpdateContact;

public record UpdateContactCommand : IRequest<OperationResult<ContactDto>>
{
    [JsonIgnore]
    public Guid ContactId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
