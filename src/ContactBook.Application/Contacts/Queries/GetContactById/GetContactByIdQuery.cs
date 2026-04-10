using ContactBook.Application.Contacts.Dtos;
using ContactBook.Domain.Shared;
using MediatR;

namespace ContactBook.Application.Contacts.Queries.GetContactById;

public record GetContactByIdQuery(Guid Id) : IRequest<OperationResult<ContactDto>>;
