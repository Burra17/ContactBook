namespace ContactBook.Application.Contacts.Dtos;

public record ContactDto(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber
);
