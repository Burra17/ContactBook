using ContactBook.Application.Contacts.Dtos;
using ContactBook.Application.Interfaces;
using ContactBook.Domain.Shared;
using MediatR;

namespace ContactBook.Application.Contacts.Queries.GetContactById;

public class GetContactByIdHandler : IRequestHandler<GetContactByIdQuery, OperationResult<ContactDto>>
{
    private readonly IContactRepository _contactRepository;

    public GetContactByIdHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<OperationResult<ContactDto>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetByIdAsync(request.Id);

        if (contact is null)
        {
            return OperationResult<ContactDto>.NotFound("Contact not found.");
        }
        
        var contactDto = new ContactDto(
            contact.Id,
            contact.Name,
            contact.Email,
            contact.PhoneNumber
        );

        return OperationResult<ContactDto>.Success(contactDto);
    }
}
