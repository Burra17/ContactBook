using ContactBook.Application.Interfaces;
using ContactBook.Domain.Models;
using ContactBook.Domain.Shared;
using MediatR;

namespace ContactBook.Application.Contacts;

public class CreateContactHandler : IRequestHandler<CreateContactCommand, OperationResult<Guid>>
{
    private readonly IContactRepository _contactRepository;

    public CreateContactHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<OperationResult<Guid>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        await _contactRepository.AddAsync(contact);

        return OperationResult<Guid>.Success(contact.Id);
    }
}
