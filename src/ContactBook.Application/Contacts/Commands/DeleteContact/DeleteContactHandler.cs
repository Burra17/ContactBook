using ContactBook.Application.Interfaces;
using ContactBook.Domain.Shared;
using MediatR;

namespace ContactBook.Application.Contacts.Commands.DeleteContact;

public class DeleteContactHandler : IRequestHandler<DeleteContactCommand, OperationResult>
{
    private readonly IContactRepository _contactRepository;
    
    public DeleteContactHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<OperationResult> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetByIdAsync(request.ContactId);

        if (contact == null)
        {
            return OperationResult.NotFound("Contact not found.");
        }

        await _contactRepository.DeleteAsync(contact);

        return OperationResult.Success();
    }
}
