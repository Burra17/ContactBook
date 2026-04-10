using ContactBook.Application.Contacts.Dtos;
using ContactBook.Domain.Shared;
using MediatR;

namespace ContactBook.Application.Contacts.Queries.GetContacts;

public record GetContactsQuery() : IRequest<OperationResult<List<ContactDto>>>;
