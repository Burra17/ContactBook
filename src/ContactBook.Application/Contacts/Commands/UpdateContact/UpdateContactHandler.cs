using ContactBook.Application.Contacts.Dtos;
using ContactBook.Application.Interfaces;
using ContactBook.Domain.Shared;
using MediatR;

namespace ContactBook.Application.Contacts.Commands.UpdateContact;

public class UpdateContactHandler : IRequestHandler<UpdateContactCommand, OperationResult<ContactDto>>
{
    private readonly IContactRepository _contactRepository;
    public UpdateContactHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<OperationResult<ContactDto>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetByIdAsync(request.ContactId);

        if (contact == null)
        {
            return OperationResult<ContactDto>.NotFound("Contact not found.");
        }

        contact.Name = request.Name;
        contact.Email = request.Email;
        contact.PhoneNumber = request.PhoneNumber;

        await _contactRepository.UpdateAsync(contact);

        var contactDto = new ContactDto(
           contact.Id,
           contact.Name,
           contact.Email,
           contact.PhoneNumber
       );

        return OperationResult<ContactDto>.Success(contactDto);
    }
}
