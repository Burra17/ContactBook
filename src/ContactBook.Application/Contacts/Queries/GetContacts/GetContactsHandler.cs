using ContactBook.Application.Contacts.Dtos;
using ContactBook.Application.Interfaces;
using ContactBook.Domain.Shared;
using MediatR;

namespace ContactBook.Application.Contacts.Queries.GetContacts;

public class GetContactsHandler : IRequestHandler<GetContactsQuery, OperationResult<List<ContactDto>>>
{
    private readonly IContactRepository _contactRepository;

    public GetContactsHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<OperationResult<List<ContactDto>>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = (await _contactRepository.GetAllAsync())
            .Select(c => new ContactDto(
                c.Id,
                c.Name, 
                c.Email, 
                c.PhoneNumber
            ))
            .ToList();

        return OperationResult<List<ContactDto>>.Success(contacts);
    }
}
