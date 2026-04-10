using ContactBook.Domain.Shared;
using MediatR;

namespace ContactBook.Application.Contacts.Commands.CreateContact;

public record CreateContactCommand(string Name, string Email, string PhoneNumber)
    : IRequest<OperationResult<Guid>>;
