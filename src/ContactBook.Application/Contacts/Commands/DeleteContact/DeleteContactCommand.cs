using ContactBook.Domain.Shared;
using MediatR;

namespace ContactBook.Application.Contacts.Commands.DeleteContact;

public record DeleteContactCommand(Guid ContactId) : IRequest<OperationResult>;
